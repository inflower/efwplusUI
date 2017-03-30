define(['jquery', 'common', "jquery.easyui", "handlebars.min", "text!../../handlebars/deptmanage.html"], function ($, common, easyui, Handlebars, html_template) {
    //
    function showpage(menuId,templates) {
        //common.simpleAjax("menu/getmenu1", {}, function (data) {

        common.loadtemplate(menuId, templates, html_template, { data: [] });
        $.parser.parse($('#content_body'));
        //});
    }

    return {
        showpage: showpage
    };
});