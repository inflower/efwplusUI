require.config({
    baseUrl: 'assets/js',
    map: {
        '*': {
            'css': 'css.min',
            'text': 'text'
        }
    },
    paths: {
        //text: 'text',
        //css: 'css.min',
        "jquery": 'jquery.min',
        "jquery.easyui": "../../uiframe/jquery-easyui-1.4.2/jquery.easyui.min",
        "easyui-lang-zh_CN": "../../uiframe/jquery-easyui-1.4.2/locale/easyui-lang-zh_CN",
        "amazeui": 'amazeui.min',
        "jquery.json": 'jquery.json-2.3.min',
        "common": '../../js/common',
        "login": "../../js/login",
        "app":"app",
        "index": '../../js/index'
    },
    shim: {
        "jquery.easyui": ["jquery", "easyui-lang-zh_CN", "css!../../uiframe/jquery-easyui-1.4.2/themes/bootstrap/easyui.css", "css!../../uiframe/jquery-easyui-1.4.2/themes/icon.css"],
        "amazeui": ["jquery"],
        "amazeui.tree.min": ["jquery", "css!../css/amazeui.tree.min.css"],
        "jquery.cookie": ["jquery"],
        "jquery.formatDateTime.min": ["jquery"],
        "jquery.json":["jquery"],
        "app": ["jquery", "amazeui"],
        "login": ["amazeui"],
        "index": ["amazeui", "app", "jquery", "jquery.cookie", "director.min"]
    },
    waitSeconds: 50
});

//requirejs(["amazeui.min","app", "index"]);