let output = document.getElementById("output")
let searchbox = document.getElementById("citysearch")
let searchbtn = document.getElementById("searchbtn")

let degC = document.getElementById("deg-C")
let degF = document.getElementById("deg-F")

let currentData = []
let degreeSym = 'C'

searchbtn.onclick = () => {
  searchForCity()
}
searchbox.onkeyup = (ev) => {
  if (ev.key != "Enter") {
    return
  }

  searchForCity()
}

// Temp change buttons0
const selectedClass = "btn-info"
const outlineClass = "btn-outline-info"

degC.classList.add(selectedClass)
degF.classList.add(outlineClass)

degC.onclick = () => {
  if (degreeSym == 'C') {
    return
  }

  degreeSym = 'C'

  degC.classList.add(selectedClass)
  degC.classList.remove(outlineClass)

  degF.classList.add(outlineClass)
  degF.classList.remove(selectedClass)

  render()
}
degF.onclick = () => {
  if (degreeSym == 'F') {
    return
  }

  degreeSym = 'F'

  degC.classList.remove(selectedClass)
  degC.classList.add(outlineClass)

  degF.classList.remove(outlineClass)
  degF.classList.add(selectedClass)

  render()
}

function searchForCity() {
  let searchStr = encodeURIComponent(searchbox.value)

  if (searchStr == "") {
    return
  }

  let url = `${window.location.origin}/openweather/query/?q=${searchStr}`;

  fetch(url)
    .then(r => r.json())
    .then(d => {
      currentData = d
      render()
    })
}

function render() {
  let htmlString = /*html*/`<div class="bg-secondary rounded h-100 p-4 mt-4"><h6>`

  if (currentData.length < 1) {
    htmlString += "Found no matching cities."
  } else {
    htmlString += `Found ${currentData.length} matching cities.`
  }

  htmlString += "</h6></div>"

  let names = new Intl.DisplayNames(['en'], {type: 'region'})

  for (let info of currentData) {
    let location = info.location
    let weather = info.current
    let main = weather.main

    let cityName = location.local_names?.en ?? location.name
    let state = location.state ? `${location.state}, ` : ""

    let tzName = weather.timezone_name
    let dtformat = new Intl.DateTimeFormat(['en'], {
      timeZone: tzName,
      dateStyle: 'full',
      timeStyle: 'full'
    })
    let sampleDate = new Date(weather.dt * 1000)

    htmlString += /*html*/`
    <div class="bg-secondary rounded h-100 p-4 mt-4">

      <h6 class="mb-4">${cityName}, ${state}${names.of(location.country)}</h6>
      <h2>${degrees(main.temp)} <span class="h6 text-muted">(Feels like ${degrees(main.feels_like)})</span></h2>

      <div class="mb-4">${dtformat.format(sampleDate)}</div>

      <dl class="row">
        ${createField("Visibility", weather.visibility >= 10000 ? null : `${weather.visibility / 1000}km`)}
        ${createField("Humidity", `${main.humidity}%`)}
        ${createField("Pressure", `${main.pressure} hPa`)}
        ${createField("Weather", weather.weather[0].description)}
        ${createField("Rain", weather?.rain?.['1h'])}
        ${createField("Snow", weather?.snow?.['1h'])}
        ${windInfo(weather.wind)}
      </dl>
    </div>
    `
  }

  output.innerHTML = htmlString
}

function degrees(num) {
  if (degreeSym == 'F') {
    num = (num * (9.0 / 5.0)) + 32.0
  }
  return `${num.toFixed(0)}&deg; ${degreeSym}`
}

function windInfo(wind) {
  if (wind == null) {
    return ""
  }

  let speed = wind.speed
  let unit = "m/s"

  if (degreeSym == 'F') {
    speed *= 2.2369362920544
    unit = "mph"
  }

  return createField("Wind", `${speed.toFixed(2)} ${unit}`)
}

function createField(fieldName, value) {
  if (value == null || value === "") {
    return ""
  }

  return /*html*/`
    <dt class="col-sm-1">${fieldName}</dt>
    <dd class="col-sm-11">${value}</dd>
  `
}