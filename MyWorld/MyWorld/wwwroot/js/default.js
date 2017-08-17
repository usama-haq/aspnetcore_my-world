(function () {
    var e = jQuery("#username");
    e.text("Sammy");

    var main = jQuery("#main");

    main.on("mouseenter", function () {
        main.style = "background-color: #888;";
    });

    main.on("mouseleave", function () {
        main.style = "";
    });

    var menuitems = jQuery("ul.menu li a");
    menuitems.on("click", function () {
        var me = jQuery(this);
        alert(me.text());
    });
})();