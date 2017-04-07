define(['jquery', 'jquery.upload', 'common', "handlebars.min", "text!../../handlebars/upgrade.html"], function ($,upload, common, Handlebars, html_template) {

    //
    function show_page(menuId, templates) {
        common.loadtemplate(menuId, templates, html_template, { data: [] });
        //中间件升级包
        $('#mnode_uploadxml').click(function () {
            upload('Upgrade/UploadMNode_updatexml');
        });
        $('#mnode_uploadzip').click(function () {
            upload('Upgrade/UploadMNode_updatezip');
        });
        $('#mnode_delete').click(function () {

        });
        //Web程序升级包
        $('#web_uploadxml').click(function () {
            upload('Upgrade/UploadWeb_updatexml');
        });
        $('#web_uploadzip').click(function () {
            upload('Upgrade/UploadWeb_updatezip');
        });
        $('#web_delete').click(function () {

        });
        //桌面程序升级包
        $('#win_uploadxml').click(function () {
            upload('Upgrade/UploadWin_updatexml');
        });
        $('#win_uploadzip').click(function () {
            upload('Upgrade/UploadWin_updatezip');
        });
        $('#win_delete').click(function () {

        });
    }

    function upload(url) {
        // 上传方法
        $.upload({
            // 上传地址
            url: common.postUrl(url),
            // 文件域名字
            fileName: 'filedata',
            // 其他表单数据
            //params: { upgradename: 'MNodeUpgrade/update.xml' },
            // 上传完成后, 返回json, text
            dataType: 'json',
            // 上传之前回调,return true表示可继续上传
            onSend: function () {
                return true;
            },
            // 上传之后回调
            onComplate: function () {
                alert("upload done");
            }
        });
    }

    return {
        showpage: show_page
    };
});