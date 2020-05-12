function setLocalStorage(obj) {
    localStorage.setItem('defaultItem', obj)
}

function getLocalStorage() {
    return localStorage.getItem('defaultItem')
}

function clearLocalStorage() {
    localStorage.removeItem('defaultItem')
}