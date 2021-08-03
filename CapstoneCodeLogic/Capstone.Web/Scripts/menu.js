$(document).ready(function () {

    //$("#button-hide").click(function () {
    //    visible = $(".name").css("display")
    //    if (visible == "none") {
    //        visible = "block"
    //        $(".name").css("display", "block")
    //        $(".navbox").css("width", "325px")
    //        $(".container").css("margin-left", "20%")
    //    }
    //    else {
    //        visible = "none"
    //        $(".name").css("display", "none")
    //        $(".navbox").css("width", "60px")
    //        $(".container").css("margin-left", "10%")
    //    }
    //});

    if (localStorage.getItem("menuStatus") === null) {
        $(".name").css("display", "block");
        $(".navpanel").css("width", "18%");
        $(".container").css("margin-left", "20%");
        $(".container").css("width", "79%");
        localStorage.setItem("menuStats", "shown");
    }
    else {
        if (localStorage.getItem("menuStatus") === "shown") {
            $(".name").css("display", "block");
            $(".navpanel").css("width", "18%");
            $(".container").css("margin-left", "20%");
            $(".container").css("width", "79%");
        }
        else {
            $(".name").css("display", "none")
            $(".navpanel").css("width", "70px")
            $(".container").css("width", "80%");
        }
    }

    ////The purpose of this if/else statement is to check the local storage to decide whether to show or hide the menu upon launch
    //if (localStorage.getItem("menuStatus") === null) {
    //    $(".name").css("display", "none")
    //    $(".navpanel").css("width", "70px")
    //    $(".container").css("width", "80%");
    //    $(".pagenavbox").css("width", "11%");
    //    localStorage.setItem("menuStats", "hidden");
    //}
    //else {
    //    if (localStorage.getItem("menuStatus") === "shown") {
    //        $(".name").css("display", "block");
    //        $(".navpanel").css("width", "18%");
    //        $(".container").css("margin-left", "20%");
    //        $(".container").css("width", "80%");
    //        $(".pagenavbox").css("width", "13%");
    //    }
    //    else {
    //        $(".name").css("display", "none")
    //        $(".navpanel").css("width", "70px")
    //        $(".container").css("width", "80%");
    //        $(".pagenavbox").css("width", "11%");
    //    }
    //}

    var visible = "none"

    $("#button-hide").click(function () {
        visible = $(".name").css("display");
        if (visible === "none") {
            visible = "block";
            $(".name").css("display", "block");
            $(".navpanel").css("width", "18%");
            $(".container").css("margin-left", "20%");
            $(".container").css("width", "79%");
            localStorage.setItem("menuStatus", "shown");
        }
        else {
            visible = "none";
            $(".name").css("display", "none");
            $(".navpanel").css("width", "70px");
            $(".container").css("margin-left", "10%");
            $(".container").css("width", "80%");
            localStorage.setItem("menuStatus", "hidden");
        }
    });

    //Un-commenting this block will enable the menu to be able to be hovered to open
    //$(".navbox").hover(function () {
    //    if (visible == "none") {
    //        $(".name").css("display", "block")
    //        $(".navbox").css("width", "325px")
    //        $(".container").css("margin-left", "20%")
    //    }
    //}, function () {
    //    if (visible == "none") {
    //        $(".name").css("display", "none")
    //        $(".navbox").css("width", "60px")
    //        $(".container").css("margin-left", "10%")
    //    }
    //});

    var width = $(window).width();
    if (width < 1200) {
        $(".name").css("display", "none");
        $(".navpanel").css("width", "70px");
        $(".container").css("width", "90%");
        $(".container").css("margin-left", "10%");
    }
});
