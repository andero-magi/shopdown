let games = []
let page = 1

const GAME_TAGS = [
    "mmorpg", "shooter", "strategy",
    "moba", "racing", "sports",
    "social", "sandbox", "open-world",
    "survival", "pvp", "pve",
    "pixel", "voxel", "zombie",
    "turn-based", "first-person", "third-person",
    "top-down", "tank", "space",
    "sailing", "side-scroller", "superhero",
    "permadeath", "card", "battle-royale",
    "mmo", "mmofps", "mmotps",
    "3d", "2d", "anime",
    "fantasy", "sci-fi", "fighting",
    "action-rpg", "action", "military",
    "martial-arts", "flight", "low-spec",
    "tower-defense", "horror", "mmorts"
]

let searchTags = []

let outputDiv = document.getElementById("freegames-out")
let dropdownMenu = document.getElementById("tag-menu")
let tagsBox = document.getElementById("tag-element")
let sortSelect = document.getElementById("sort-select")
let platformSelect = document.getElementById("platform-select")

dropdownMenu.style.maxHeight = "33.33vh"
dropdownMenu.style.overflowY = "scroll"

class GameTag {
    constructor(tag, dropdownMenuElement) {
        this.tag = tag
        this.dropdownMenuElement = dropdownMenuElement
    }

    pushTag() {
        if (searchTags.indexOf(this.tag) != -1) {
            return
        }

        this.dropdownMenuElement.classList.add("text-muted")
        searchTags.push(this.tag)

        let el = this.getTagBoxElement()
        tagsBox.appendChild(el)
    }

    removeTag() {
        this.dropdownMenuElement.classList.remove("text-muted")

        let el = this.tagBoxElement

        if (el != null) {
            tagsBox.removeChild(el)
            this.tagBoxElement = null
        }

        let idx = searchTags.indexOf(this.tag)
        if (idx != -1) {
            searchTags.splice(idx)
        }
    }

    getTagBoxElement() {
        let div = document.createElement("div")
        div.className = "btn btn-danger m-2"
        div.textContent = this.tag

        let t = this

        div.onclick = () => {
            t.removeTag()
        }

        this.tagBoxElement = div

        return div
    }
}

for (let tag of GAME_TAGS) {
    let el = document.createElement("div")
    el.className = "dropdown-item text-white"
    el.setAttribute("value", tag)
    el.textContent = `${tag}`

    let t = new GameTag(tag, el)

    el.onclick = () => t.pushTag()
    dropdownMenu.appendChild(el)
}

updateGames()

function queryGames() {
    let url = `${window.location.origin}/freegames/query?`

    if (searchTags.length > 0) {
        let tagsParam = searchTags.map(encodeURIComponent).join('&Tags=')
        url += `Tags=${tagsParam}`
    }

    url += `&Platform=${encodeURIComponent(platformSelect.value)}`
    url += `&Sort=${encodeURIComponent(sortSelect.value)}`

    console.log(url)
    console.log(searchTags)
    console.log(platformSelect.value)
    console.log(sortSelect.value)

    return fetch(url)
        .then(r => r.json())
        .then(j => {
            page = 1
            games = j
            console.log(j)
        })
}

function renderGames() {
    let str = ``

    for (let i = 0; i < games.length; i++) {
        let g = games[i]
        str += renderGameToHtmlString(g)
    }

    outputDiv.innerHTML = `
    <h6 class="mb-4">Games</h6>
    <div style="display: flex; flex-wrap: wrap; justify-content: center">
      ${str}
    </div>
    `
}

function renderGameToHtmlString(game) {
    return /*HTML*/`
    <div class="card bg-dark mb-4 mx-2" style="width: 18rem;">
       <div class="card-body d-flex flex-column">
         <img src="${game.thumbnail}" class="card-img-top">
         <h5 class="card-title mt-4">${game.title}</h5>
         <p class="card-text mb-4">${game.short_description}</p>
         <a class="btn btn-primary align-self-start" style="margin-top:auto;" href="${game.game_url}">Game Page<a/>
       </div>
    </div>
    `
}

function updateGames() {
  queryGames().then(() => renderGames())
}