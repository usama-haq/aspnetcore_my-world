﻿(function () {
    var e = document.getElementById("username");
    e.innerHTML = "Sammy";

    var main = document.getElementById("main");
    main.onmouseenter = function () {
        main.style.backgroundColor = "#888";
    };

    main.onmouseleave = function () {
        main.style.backgroundColor = "";
    };
})();