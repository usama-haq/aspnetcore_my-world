(function () {
    var sidebarAndWrapper = jQuery("#sidebar,#wrapper");

    var sidebarToggleButton = jQuery("#sideBarToggleButton");

    sidebarToggleButton.on("click", function () {
        sidebarAndWrapper.toggleClass("hide-sidebar");

        if (sidebarAndWrapper.hasClass("hide-sidebar")) {
            jQuery(this).text("Show Sidebar");
        }
        else {
            jQuery(this).text("Hide Sidebar");
        }
    });

})();