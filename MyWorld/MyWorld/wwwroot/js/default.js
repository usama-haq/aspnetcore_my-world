(function () {
    var sidebarAndWrapper = jQuery("#sidebar,#wrapper");

    var sidebarToggleButton = jQuery("#sideBarToggleButton");
    var icon = jQuery("#sideBarToggleButton i.fa")

    sidebarToggleButton.on("click", function () {
        sidebarAndWrapper.toggleClass("hide-sidebar");

        if (sidebarAndWrapper.hasClass("hide-sidebar")) {
            icon.removeClass("fa-angle-left");
            icon.addClass("fa-angle-right");
        }
        else {
            icon.removeClass("fa-angle-right");
            icon.addClass("fa-angle-left");
        }
    });
})();