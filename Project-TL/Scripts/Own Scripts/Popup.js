$("a").hover(function (e) {
    $($(this).data("tooltip")).css({
        left: e.pageX + 5,
        top: e.pageY + 5
    }).stop().show(100);
}, function () {
    $($(this).data("tooltip")).hide();
});
