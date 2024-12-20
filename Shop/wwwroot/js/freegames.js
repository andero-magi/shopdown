﻿let games = []

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
].sort()

let searchTags = []

let outputDiv = document.getElementById("freegames-out")
let dropdownMenu = document.getElementById("tag-menu")
let tagsBox = document.getElementById("tag-element")
let sortSelect = document.getElementById("sort-select")
let platformSelect = document.getElementById("platform-select")

dropdownMenu.style.maxHeight = "33.33vh"
dropdownMenu.style.overflowY = "scroll"

outputDiv.onclick = (ev) => {
    let t = ev.target
    
    if (t.tagName == "a") {
        return
    }

    while (!t.hasAttribute("game-url")) {
      if (t == outputDiv) {
        return
      }

      t = t.parentElement
    }

    let url = t.getAttribute("game-url")
    window.open(url, "_blank").focus()
}

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
            searchTags.splice(idx, 1)
        }
    }

    getTagBoxElement() {
        let div = document.createElement("div")
        div.className = "btn btn-danger m-2 tag-remove"
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

function getQueryUrl() {
    let url = `${window.location.origin}/freegames/query?`

    if (searchTags.length > 0) {
        let tagsParam = searchTags.map(encodeURIComponent).join('&Tags=')
        url += `Tags=${tagsParam}`
    }

    url += `&Platform=${encodeURIComponent(platformSelect.value)}`
    url += `&Sort=${encodeURIComponent(sortSelect.value)}`

    return url
}

function queryGames() {
    let url = getQueryUrl()

    return fetch(url)
        .then(r => r.json())
        .then(j => {
            games = j
        })
}

function renderGames() {
    let str = ``

    for (let i = 0; i < games.length; i++) {
        let g = games[i]
        str += renderGameToHtmlString(g)
    }

    outputDiv.innerHTML = /*HTML*/`
    <h6 class="mb-4">Games (${games.length} results)</h6>
    <div style="display: flex; flex-wrap: wrap; justify-content: center">
      ${str}
    </div>
    `
}

function renderGameToHtmlString(game) {
    let publisher = game.publisher
    let dev = game.developer
    let credit

    if (dev == publisher) {
        credit = ` by ${dev}`
    } else {
        credit = `, published by ${publisher} and developed by ${dev}`
    }

    return /*HTML*/`
    <div game-url="${game.game_url}" class="card game-card bg-secondary mb-4 mx-2" style="width: 18rem;">
       <div class="card-body d-flex flex-column">
         <img src="${game.thumbnail}" class="card-img-top">
         <h5 class="card-title mt-4">${game.title} <i class="text-muted">(${game.release_date.substring(0, 4)})</i></h5>
         <p class="card-text">${game.short_description}</p>
         <p class="card-text mb-4">${game.genre}${credit}</p>
         <a class="btn btn-primary align-self-start" style="margin-top:auto;" href="${game.game_url}" target="_blank">Game Page<a/>
       </div>
    </div>
    `
}

function updateGames() {
  queryGames().then(() => renderGames())
}