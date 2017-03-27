define(['jquery', 'common', "handlebars.min", "text!../../handlebars/menu1.html"], function ($, common, Handlebars, html_template) {

    //通用
    function show_common(menuId, para, urls, templates, callback, errorcallback) {
        if (!urls[menuId] || !templates[menuId]) {
            $('#content_body').html(html_template);//加载html模板文本
            //设置多个url和模板
            urls[menuId] =  para;

            templates[menuId] = Handlebars.compile($("#" + menuId + "-template").html());
        }

        common.simpleAjax(urls[menuId], {}, function (data) {
            var context = { data: common.toJson(data) };
            var html = templates[menuId](context);
            $('#content_body').html(html);

            if (callback) {
                callback(data);
            }
        }, errorcallback);
    }

    //
    function show_page(menuId, urls, templates) {
        show_common(menuId, "Hello/test", urls, templates);
    }

    return {
        showpage: show_page
    };
});