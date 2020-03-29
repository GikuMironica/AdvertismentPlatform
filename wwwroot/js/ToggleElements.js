function toggle_visibility(id, category) {
    var e = document.getElementById(id);

    if (e.style.display == 'block')
        e.style.display = 'none';
    else
        e.style.display = 'block';

    $('.hideble').not('#' + id).hide();
    $('#selectedItemTypevalue').text(category);
}