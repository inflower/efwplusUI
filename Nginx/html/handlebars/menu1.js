﻿define(['jquery', 'common', "handlebars.min", "text!../../handlebars/menu1.html"], function ($, common, Handlebars, html_template) {
    //
    function showpage(menuId,templates) {
        common.simpleAjax("menu/getmenu1", {}, function (data) {

            common.loadtemplate(menuId, templates, html_template, { data: common.toJson(data) });
        });
    }

    return {
        showpage: showpage
    };
});