define(["handlebars.min", "common", "text!../../handlebars/menu.html"], function (Handlebars, common, html_template) {
    
    var labmenus;//标签菜单显示
    //var urls;//模板数据请求地址
    var templates;//模板内容
    //urls = new Array();
    templates = new Array();//handlebars模板对象
    labmenus = new Array();

    //初始化
    function init () {
        //common.validateuser();//身份验证

        require(["../../handlebars/welcome"], function (page) {
            page.showpage("welcome", templates);
        });

        $('#username').text($.cookie("username"));
        loadsysmenus();
        loadrouter();

        //$('#content_body').height(function () {
        //    return this.parent.outerHeight();
        //});
        
    }

   
    //加载路由
    function loadrouter() {
        //菜单通过路由方式打开页面
        var opencontent = function (menuId) {
            //console.log("loadcontent:" + menuId);
            if (menuId == 'quit') {
                window.location.href = 'login.html';
            } else {
                showpage(menuId);
            }
        };

        var routes = {
            '/openmenu/:menuId': [opencontent]
        };
        var router = Router(routes);
        router.init();
    }

    //加载系统菜单
    function loadsysmenus() {
        var sysmenus;//系统菜单
        //系统菜单Json对象
        sysmenus = [{
            "moudleid": "syssetting", "moudlename": "系统设置", "child": [
                { "Id": "workermanage", "Name": "机构管理" },
                { "Id": "deptmanage", "Name": "科室管理" },
                { "Id": "empmanage", "Name": "人员管理" },
                { "Id": "usermanage", "Name": "用户管理" },
                { "Id": "modulemanage", "Name": "模块管理" },
            { "Id": "menumanage", "Name": "菜单管理" },
            { "Id": "groupmenu", "Name": "角色权限" }
            ]
        }];

        //$.each(sysmenus, function (i, n) {
        //    $.each(n.child, function (k, m) {
        //        labmenus[m.Id] = [n.moudlename, m.Name];
        //    });
        //});

        $('#content_body').html(html_template);//加载html模板文本

        //显示系统菜单
        var menu_tpl = Handlebars.compile($("#menu-template").html());
        var menu_html = menu_tpl(sysmenus);
        $('#sysmenus').html(menu_html);

        $('#content_body').html("");//清空
    }

    //动态加载页面
    function showpage(menuId) {
        $('#content_body').html("");//先清空
        $('#lastfoot').nextAll().remove();

        require(["../../handlebars/" + menuId], function (page) {
            page.showpage(menuId, templates);
        });
    }

    return {
        init: init
    };
});