function toggle_visibility(id, category) {
    $('#' + id).slideDown();
    $('.hideble').not('#' + id).slideUp();
    $('#selectedItemTypevalue').text(category);
}