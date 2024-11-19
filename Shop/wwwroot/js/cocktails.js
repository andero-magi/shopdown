let searchInput = document.getElementById("search-in")
let output = document.getElementById("output")

let cocktails = []
let currentSearch = ""

searchInput.onkeyup = (ev) => {
    if (ev.key != "Enter") {
        return
    }

    onSearch()
}
output.onclick = (ev) => {
    let t = ev.target

    if (!t.hasAttribute("drink-index")) {
        return
    }
    if (t.getAttribute("disabled") == "true") {
        return
    }

    let idx = parseInt(t.getAttribute("drink-index"))
    saveToDatabase(idx)
    t.setAttribute("disabled", "true")
}

initSearch()

function initSearch() {
    let p = new URLSearchParams(window.location.search)
    let q = p.get("q")

    if (q == null || q == "") {
        showSaved()
        return
    }

    searchInput.value = q
    onSearch()
}

function showSaved() {
    let url = `${window.location.origin}/cocktail/saved`

    window.history.pushState(null, "", "/cocktail")
    currentSearch = ""

    fetch(url)
        .then(r => r.json())
        .then(d => {
            cocktails = d
            displayCocktails()
        })
}

function onSearch() {
    let searchStr = encodeURIComponent(searchInput.value)

    if (searchStr == "") {
        return
    }

    let url = `${window.location.origin}/cocktail/query?query=${searchStr}`

    currentSearch = searchStr
    window.history.pushState(null, "", `/cocktail?q=${searchStr}`)

    fetch(url)
        .then(r => r.json())
        .then(d => {
            cocktails = d
            displayCocktails()
        })
}

function displayCocktails() {

    let out = ""

    out += `<div class="bg-secondary rounded h-100 p-4 mt-4"><h6>`

    if (currentSearch == "") {
        if (cocktails.length < 1) {
            out += `You have no saved drinks.`
        } else {
            out += `Found ${cocktails.length} saved drinks.`
        }
    } else {
        if (cocktails.length < 1) {
            out += `Found no cocktails matching your search.`
        } else {
            out += `Found ${cocktails.length} drinks matching the search.`
        }
    }

    out += "</h6></div>"

    function createField(name, val) {
        return `
        <dt class="col-sm-1">${name}</dt>
        <dd class="col-sm-11">${val}</dd>
        `
    }

    function imgElement(drink) {
        if (drink.image == null) {
            return ""
        }

        let src = `${drink.image}/preview`

        return `<img class="mb-4 rounded" src="${src}">`
    }

    function ingredientElement(ing) {
        let srcName = ing.name.replace(" ", "%20")
        let imgUrl = `https://www.thecocktaildb.com/images/ingredients/${srcName}-Small.png`
        return `<img class="bg-dark rounded p-2" src="${imgUrl}"> ${ing.measurement}${ing.name}`
    }

    for (let idx in cocktails) {
        let drink = cocktails[idx]

        let drinkEl = `
          <div class="bg-secondary rounded h-100 p-4 mt-4">
          <h5 class="mb-4">${drink.name}</h5>

          ${imgElement(drink)}
          
          <h6>Details</h6>
          <dl class="row">
            ${createField("Category", drink.category)}
            ${createField("Alcaholic", drink.alcaholic)}
            ${createField("Glass", drink.glass)}
            ${createField("IBA", drink.iba)}
          </dl>
        `

        let ingList = drink.ingredients
        if (ingList != null && ingList.length > 0) {
            drinkEl += "<h6>Ingredients</h6> <ul>"

            for (let ing of ingList) {
                drinkEl += `<li class="mb-2">${ingredientElement(ing)}</li>`
            }

            drinkEl += "</ul>"
        }

        drinkEl += `
            <h6>Instructions</h6>
            ${drink.instructions_en}
        `

        drinkEl += `
          <button drink-index="${idx}" ${drink.saved ? "disabled" : ""} class="btn btn-primary d-block mt-4">Save to Database</button>
        </div>`
        out += drinkEl
    }

    output.innerHTML = out
}

function saveToDatabase(idx) {
    let drink = cocktails[idx]
    if (drink == null) {
        return
    }

    if (drink.saved) {
        return
    }

    let saveUrl = `${window.location.origin}/cocktail/save/${drink.id}`
    fetch(saveUrl, {method: "POST"})
}