/* Toggle show nav sidebar */
function toggleSidebar() {
    $('.sidebar-nav').toggleClass('show');
    $('.modal-main').toggleClass('d-block');
}

/* Handle click modal */
function clickModal(e) {
    $('.sidebar-nav').removeClass('show');
    e.classList.remove('d-block');
}