
//实现元素的
var resizseable = {
    position: function (ele, e) {//鼠标的位置, ele: 元素，e: 元素move鼠标事件对象
        var width = ele.offsetWidth;
        var height = ele.offsetHeight;
        var offsetX = e.offsetX;
        var offsetY = e.offsetY;

        if (offsetX >= width - 10 && offsetY < height - 10) {//右
            return 1;
        }
        if (offsetX >= width - 10 && offsetY >= height - 10) {//右下
            return 2;
        }
        if (offsetX < width - 10 && offsetY >= height - 10) {//下
            return 3;
        }
        return 0;
    },
    mousemove: function (e) {//鼠标移入时，改变大小 e:元素的move事件对象
        var target = e.target;

        var resizeCache = resizseable.resizeCache;
        if (resizseable.position(target, e) === 1) {//右
            target.style.cursor = "e-resize";
            resizeCache.derection = 1; return;
        }
        if (resizseable.position(target, e) === 2) {//右下
            target.style.cursor = "se-resize";
            resizeCache.derection = 2; return;
        }
        if (resizseable.position(target, e) === 3) {//下
            target.style.cursor = "n-resize";
            resizeCache.derection = 3; return;
        }
        target.style.cursor = "inherit";
        resizeCache.derection = 0;
    },
    mousedown: function (e) {//鼠标按下的时候  当鼠标在元素中按下
        var target = e.target;
        if (resizseable.position(target, e) === 0)
            return;

        var resizeCache = resizseable.resizeCache;
        resizeCache.resizeTarget = e.target;
        resizeCache.resizeTarget.onmousemove = null;
        resizeCache.clickX = e.pageX;
        resizeCache.clickY = e.pageY;
        resizeCache.targetWidth = target.offsetWidth;
        resizeCache.targetHeight = target.offsetHeight;
    },
    resizeEle: function (ele, oldwidth, oldheigth, offsetx, offsety) {//改变元素大小  ele ：元素 ,oldwidth ： 元素改变前width ， oldheight : 元素改变后 height ， offsetx ： 鼠标的x位移，鼠标y的位移
        if (oldwidth <= 20 && oldheigth <= 20)
            return;

        if (offsetx === 0 && oldheigth > 20 && oldheigth + offsety > 20) {
            ele.style.height = (oldheigth + offsety) + "px";
        }
        else if (offsety === 0 && oldwidth > 20 && oldwidth + offsetx > 20) {
            ele.style.width = (oldwidth + offsetx) + "px";
        }
        else if (oldheigth + offsety > 20 && oldwidth + offsetx > 20) {
            ele.style.height = (oldheigth + offsety) + "px";
            ele.style.width = (oldwidth + offsetx) + "px";
        }

    },
    resizeCache : {//记录改变大小的元素的信息
        resizeTarget: null, //改变大小的元素
        targetWidth: 0,
        targetHeight: 0,
        derection: 0,
        clickY: 0,
        clickX: 0,
    },
    docOnmouseup: function (e) {//dom的事件
        var eleCache = resizseable.resizeCache;
        if (eleCache.resizeTarget === null)
            return;

        eleCache.derection = 0;
        eleCache.resizeTarget.style.cursor = "inherit";
        eleCache.resizeTarget.onkeydown = null;
        eleCache.resizeTarget.onmousemove = resizseable.mousemove;
        eleCache.resizeTarget = null;

    },
    docOnmousemove: function (e) {//dom的事件
        var eleCache = resizseable.resizeCache;
        if (eleCache.resizeTarget === null)
            return;

        var pageX = e.pageX;
        var pageY = e.pageY;

        var direc = eleCache.derection;
        if (direc === 1) {
            resizseable.resizeEle(eleCache.resizeTarget, eleCache.targetWidth, eleCache.targetHeight, pageX - eleCache.clickX, 0);
        }
        else if (direc === 2) {
            resizseable.resizeEle(eleCache.resizeTarget, eleCache.targetWidth, eleCache.targetHeight, pageX - eleCache.clickX, pageY - eleCache.clickY);
        }
        else if (direc === 3) {
            resizseable.resizeEle(eleCache.resizeTarget, eleCache.targetWidth, eleCache.targetHeight, 0, pageY - eleCache.clickY);
        }
    }
}

//获取文件扩展名
getFileExtend = function (fileName) {
    if (fileName == null || fileName.trim() == "")
        return null;
    var index = fileName.lastIndexOf(".");
    if (index<= 0)
        return null;
    return fileName.substring(index);
}
//生成图片的html
createImg = function (fileName) {
    //BMP、JPG、JPEG、PNG、GIF
    var extend = getFileExtend(fileName);
    if (extend == null)
        return null;
    if (extend !== ".bmp" && extend !== ".jpg" && extend !== ".jpeg" && extend !== ".gif" && extend !== ".png")
        return null;

    //资源url路径
    var fileUrl = aditorVms.resourceCtrl.vm.filesUrl;

    return "<img class='showBroder' draggable='false'  src='" + fileUrl + fileName + "' style='padding:10px;'  onmousemove='resizseable.mousemove(event)' onmousedown = 'resizseable.mousedown(event)' /><br/><br/>";
}
//生成音乐
createMis = function (fileName) {
    var extend = getFileExtend(fileName);
    if (extend == null)
        return null;
    if (extend !== ".ogg" && extend !== ".mp3" && extend !== ".wav")
        return null;

    //资源url路径
    var fileUrl = aditorVms.resourceCtrl.vm.filesUrl;

    return "<div align='center' contenteditable='true'>\
                    <audio controls  class='showBroder'  onmousemove='resizseable.mousemove(event)' onmousedown = 'resizseable.mousedown(event)'>\
                        <source src='"+ fileUrl + fileName +"' >\
                        < code > 你的浏览器不兼容此音乐的格式。</code >\
                    </audio >\
               </div></br>";
}
//生成视频
createVideo = function (fileName) {
    var extend = getFileExtend(fileName);
    if (extend == null)
        return null;
    if (extend !== ".mp4" && extend !== ".ogg" && extend !== ".webm")
        return null;

    //资源url路径
    var fileUrl = aditorVms.resourceCtrl.vm.filesUrl;

    return "<div align='center' contenteditable='true'>\
                    <video  controls  style='width:66%;padding:0px' class='showBroder' onmousemove='resizseable.mousemove(event)' onmousedown = 'resizseable.mousedown(event)'>\
                            <source src='"+ fileUrl + fileName + "'  style='width:100%;height:100%;'>\
                            <code> 你的浏览器不兼容此视频的格式。</code >\
               </video></div><br/>";
}
//默认的连接
createDefaultHtml = function (fileName) {
    var fileUrl = aditorVms.resourceCtrl.vm.filesUrl;
    return "<a href='" + fileUrl + fileName +"' target='_blank'>" + fileName+"</a>&nbsp;";
}


var aditorVms = {
    articleInfo: null,
    resourceCtrl: null,
    toolbar:null,
};
aditorVms.articleInfo = {
    vm: null,
    data: {
        BlogTypeChange: {
            BlogType: [],
            BlogTypeID: 0,
        },
        SingleTypeChange: {
            SingleType: [],
            SingleTypeID: 0,
        },
        Updata: {
            WorksPath: $("input#worksPath").val(),
        },
        ArticleInfo: {
            Title: "",
            imageUrl: "",
            BlogTypeID: 0,
            SingleBlogTypeID: 0,
            Introduc: "",
            Content:""
        }
    },
    methods: {
        handleAvatarSuccess: function (res, file) {//文章封面上传成功后
            this.ArticleInfo.imageUrl = URL.createObjectURL(file.raw);
        },
        beforeAvatarUpload: function (file) {//图片上传之前的验证
            const isJPG = file.type === 'image/jpeg';
            const isLt2M = file.size / 1024 / 1024 < 2;

            if (!isJPG) {
                this.$message.error('上传头像图片只能是 JPG 格式!');
            }
            if (!isLt2M) {
                this.$message.error('上传头像图片大小不能超过 2MB!');
            }
            return isJPG && isLt2M;
        },
        SaveArticle: function () {//保存文章信息
            var SaveInfo = {};
            SaveInfo.BlogWorksID = $("input#worksID").val();
            SaveInfo.Title = this.ArticleInfo.Title;
            SaveInfo.BlogTypeID = this.BlogTypeChange.BlogTypeID > 0 ? this.BlogTypeChange.BlogTypeID : null;
            SaveInfo.SingBlogTypeID = this.SingleTypeChange.SingleTypeID > 0 ? this.SingleTypeChange.SingleTypeID : null;
            SaveInfo.Introduc = this.ArticleInfo.Introduc;
            SaveInfo.CharLeng = editor.txt.text().length;
            SaveInfo.Content = editor.txt.html();
            
            var that = this;
            $.ajax({
                url: "/ArticleWrite/SaveArticle",
                data: SaveInfo,
                type: "POST",
                success: function (data) {
                    if (data.IsOk) {
                        that.$message({ message: "保存成功", type :"success"});
                        return;
                    }
                    that.$message({ message: "保存失败", type: "error" });
                }
            });

        }
    },
    created: function () {
        var that = this;
        //加载博客类型
        $.ajax({
            url: "/ArticleWrite/GetBlogType",
            data: {
                BlogWorksID: $("input#worksID").val()
            },
            type: "GET",
            success: function (data) {
                that.BlogTypeChange.BlogType = data.BlogType;
                that.BlogTypeChange.BlogTypeID = data.BlogTypeID;
            },
            complete: function () { },
        })
         //加载博客类型个人博客类型
        $.ajax({
            url: "/ArticleWrite/GetSingleBlogType",
            data: {
                BlogWorksID: $("input#worksID").val(),
                BlogID: $("input#blogID").val(),
            },
            type: "GET",
            success: function (data) {
                that.SingleTypeChange.SingleType = data.SingleType;
                that.SingleTypeChange.SingleTypeID = data.SingleTypeID == null ? 0 : data.SingleTypeID;
            },
            complete: function () { },
        })

        //加载文章信息
        $.ajax({
            url: "/ArticleWrite/GetArticleInfo",
            data: {
                BlogWorksID: $("input#worksID").val(),
            },
            success: function (data) {
                that.ArticleInfo.Title = data.Article.Title;
                that.ArticleInfo.imageUrl = data.Article.WorksPath.lastIndexOf(".jpg") > 0 ? data.Article.WorksPath : imgurl;
                that.ArticleInfo.Introduc = data.Article.Introduc;
            }
        })
    }
};
aditorVms.resourceCtrl = {
    vm: null,
    data: {
        filesUrl: "",
        files: [],
    },
    methods: {
        getAllResource: function () {//获取当前文件夹的所有资源
            var that = this;
            $.ajax({
                url: "/ArticleWrite/GetAllResource",
                data: {
                    WorksPath: $("input#worksPath").val(),
                },
                success: function (data) {
                    for (var i = 0; i < data.Files.length; i++) {
                        that.files.push({ title: data.Files[i] });
                    }
                    that.filesUrl = data.FileUrl;
                }
            });
        },
        upLoadResource: function (files) {//上传文件列表
            if (files.length <= 0 )
                return;

            var that = this;

            var formData = new FormData();//序列化文件
            formData.append("WorksPath", $("input#worksPath").val());
            for (var i = 0; i < files.length; i++) {
                if (files[i].size <= 0) continue;
                formData.append("Files", files[i]);
            }

            var msg = that.$message({ message: "文件正在上传中 . . . . .", type: "success", duration: 0 });//消息提示

            $.ajax({
                url: "/ArticleWrite/UpLoadFileList",
                data: formData,
                contentType: false,
                processData: false,
                type: "POST",
                success: function (data) {
                    if (data.IsOk) {
                        for (var i = 0; i < files.length; i++) {
                            if (that.files.some(function (val) { return val.title == files[i].name }))
                                continue;
                            that.files.push({ title: files[i].name });
                        }
                    }
                },
                complete: function () {
                    msg.close();
                }
            });
        },
        clickUpLoad: function (e) {//点击按钮上传资源
            this.upLoadResource(e.target.files);
        },
        dragenter: function (e) {//放置
            e.preventDefault();
        },
        dragover: function (e) {//放置
            e.preventDefault();
        },
        dropUp: function (e) {//上传文件
            e.preventDefault();
            var files = e.dataTransfer.files
            this.upLoadResource(files);
        },
        dropRemove: function (e) {//移除文件

            e.stopPropagation();//取消事件进一步冒泡或捕获
            var fileName = e.dataTransfer.getData("title");
            if (fileName == "" || fileName == null)
                return;

            var that = this;
            var msg = that.$message({ message: "文件正在删除中 . . . . .", type: "error", duration: 0 });//消息提示
            $.ajax({
                url: "/ArticleWrite/RemoveResource",
                data: {
                    fileName: fileName,
                    WorksPath: $("input#worksPath").val(),
                },
                type: "POST",
                success: function (data) {
                    if (data.IsOk) {//服务器移除成功则移除选项
                        for (var i = 0; i < that.files.length; i++) {
                            if (that.files[i].title === fileName) {
                                that.files.splice(i, 1);
                                break;
                            }
                        }
                    }
                },
                complete: function () {
                    msg.close();
                }
            });

        },
        dragstart: function (title) {//拖动资源的时候
            event.dataTransfer.setData("title", title);
        },
    },
    created: function () {
        this.getAllResource();
    }
};
aditorVms.toolbar = {
    vm: null,
    data: {
        isFixed: false,
    },
    methods: {
        save: function () {
            aditorVms.articleInfo.vm.SaveArticle();
        },
        fixed: function () {
            var prent = $("div#editArea");
            var eidtro = $("div#editArea>div.w-e-text");
           
            if (!this.isFixed) {
                eidtro.css("overflow-y", "scroll");
                eidtro.css("height", eidtro.height() + "px");
            } else {
                eidtro.css("overflow-y", "auto");
                eidtro.css("height", "auto");
            }
            this.isFixed = !this.isFixed;
        },
    }
}

//文章的保存与控制
aditorVms.articleInfo.vm = new Vue({
    el: "div#ArticleInfo",
    data: aditorVms.articleInfo.data,
    methods: aditorVms.articleInfo.methods,
    created: aditorVms.articleInfo.created,
})
//资源控制
aditorVms.resourceCtrl.vm = new Vue({
    el: "div#resourceCtrl",
    data: aditorVms.resourceCtrl.data,
    methods: aditorVms.resourceCtrl.methods,
    created: aditorVms.resourceCtrl.created
});
//工具栏
aditorVms.toolbar.vm = new Vue({
    el: "div#toolbar",
    data: aditorVms.toolbar.data,
    methods: aditorVms.toolbar.methods,
});

//编辑器加载
function loadEditor() {
    var E = window.wangEditor;
    editor = new E("div#worksCtrl", "div#editArea");
    editor.customConfig.lang = {
        '插入代码': '自定义html'
    }
    editor.customConfig.zIndex = 1;
    editor.create();
    //设置编辑器的y滚动规则
    var auto = $("div#editArea>div.w-e-text");
    auto.css("overflow-y", "auto");
    auto.addClass("scrollbar");
    //移除内容嵌套的容器
    var ctaine = $("div#removeContainer");
    ctaine.replaceWith(ctaine.html());
    
}

//实现编辑区html添加
function addHtml() {
    //编辑区
    var eidtorArea = document.getElementById("editArea");
    eidtorArea.ondrop = function (e) {
        e.preventDefault();
        var filename = e.dataTransfer.getData("title");//获取文件名

        var imgStr = createImg(filename);
        var audioStr = createMis(filename);
        var videoStr = createVideo(filename);

        var htmlStr = imgStr !== null ? imgStr : audioStr != null ? audioStr : videoStr !== null ? videoStr : createDefaultHtml(filename);
        editor.txt.append(htmlStr);
    }

}

addHtml();
loadEditor();

//在系统退出前提示用户保存
window.onbeforeunload = function () {
    //event.returnValue = "系统不会自动保存你的文章信息。";
    return "系统不会自动保存你的文章信息。";
}
//当按了ctrl+s保存文章
document.onkeydown = function (e) {
    if (e.ctrlKey && e.keyCode == 83) {
        e.preventDefault();
    }
}
document.onkeyup = function (e) {
    if (e.ctrlKey && e.keyCode == 83) {
        aditorVms.articleInfo.vm.SaveArticle();
    }
}

//用于改变元素大小
document.addEventListener("mousemove", resizseable.docOnmousemove);
document.addEventListener("mouseup", resizseable.docOnmouseup);

