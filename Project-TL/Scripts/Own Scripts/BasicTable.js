$(document).ready(function () {
    var table = $('table.display').DataTable();

    //Function for the 'select all' checkbox
    var allPages = table.cells().nodes();
    $('body').on('click', '#select_all', function () {
        if ($(this).hasClass('allChecked')) {
            $('input[type="checkbox"]', allPages).prop('checked', false);
        } else {
            $('input[type="checkbox"]', allPages).prop('checked', true);
        }
        $(this).toggleClass('allChecked');
    });
});