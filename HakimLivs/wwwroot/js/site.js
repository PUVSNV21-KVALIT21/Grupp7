// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

function updateCartIcon() {
    cartQuantity = localStorage.getItem('cartQuantity')
    if (!cartQuantity) {
        localStorage.setItem('cartQuantity', 0)
    }
    document.getElementById('cart-counter').textContent = cartQuantity
}

function addProductToLocalStorage(id) {
    // {'productId': '<quantity>'}
    currentProductQuantity = localStorage.getItem(id)
    if (currentProductQuantity) {
        localStorage.setItem(id, Number(currentProductQuantity) + 1)
    }
    else {
        localStorage.setItem(id, 1)
    }
    newCartQuantity = Number(localStorage.getItem('cartQuantity')) + 1
    localStorage.setItem('cartQuantity', newCartQuantity)

    updateCartIcon()
}

