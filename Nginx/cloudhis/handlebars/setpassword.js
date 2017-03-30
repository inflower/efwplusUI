define(['jquery', 'common', "jquery.easyui", "handlebars.min", "text!../../handlebars/setpassword.html"], function ($, common, easyui, Handlebars, html_template) {

    function showpage(menuId, templates) {
        //common.simpleAjax("menu/getmenu4", {}, function (data) {
        common.loadtemplate(menuId, templates, html_template, { data: [] });
        $.parser.parse($('#content_body'));
        //setHeight();
        //});
    }
    function setHeight() {
        var c = $('#setpassword');
        var p = c.layout('panel', 'center');    // get the center panel
        var oldHeight = p.panel('panel').outerHeight();
        p.panel('resize', { height: 'auto' });
        var newHeight = p.panel('panel').outerHeight();
        c.layout('resize', {
            height: (c.height() + newHeight - oldHeight)
        });
    }
    return {
        showpage: showpage
    };
});