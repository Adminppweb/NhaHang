var editorSetting = function (element, eleElfinder) {
    this.currentHashes = "";
    Init();
    setUpElfinder();
    function setUpElfinder() {
        elFinder.prototype.commands.addtoEditor = function () {
            this.exec = function (hashes) {
                this.currentHashes = hashes;
            }
            this.getstate = function () {
                return 0;
            }
        };
        let toolbar = [
            ['home'],
            ['addtoEditor', 'customTool'],
            ['upload'],
            ['back', 'forward'],
            ['reload'],
            ['mkdir', 'mkfile'],
            ['open', 'download'],
            ['undo', 'redo'],
            ['info'],
            ['quicklook'],
            ['copy', 'cut', 'paste'],
            ['rm'],
            ['duplicate', 'rename', 'edit'],
            ['selectall', 'selectnone', 'selectinvert'],
            ['view', 'sort'],
            ['search'],
        ];
        let options = {
            debug: false,
            url: '/el-finder/file-system/connector',
            //uploadURL: "/el-finder/file-system/UploadFile",
            rememberLastDir: true,
            uiOptions: {
                toolbar: toolbar
            },
        };
        var elfinder = $(eleElfinder).elfinder(options).elfinder('instance');
    }
    function selectedFiles(editor) {
        $.ajax({
            url: "/el-finder/file-system/file/" + this.currentHashes,
            type: 'GET',
            success: function (result) {
                if (result != undefined) {
                    if (!result.isDirectory) {
                        let alias = result.rootVolume.alias;
                        let insertSrc = `<img width="374" height="400" src="` + alias + '\\' + result.relativePath + `"></img>`;
                        editor.insertContent(insertSrc);
                    }
                    else {
                        console.log(result);
                    }
                }
            },
            error: function (error) {
                ///// console.log(error);
            }
        });
    }
    function Init() {
        tinymce.init({
            selector: element,
            toolbar: [
                'undo redo | formatselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | elFinder'
            ],
            plugins: 'paste',
            setup: function (editor) {
                console.log(editor);
                ////// Editor
                editor.ui.registry.addToggleButton("elFinder"
                    , {
                        icon: 'embed-icon',
                        tooltip: 'Insert Embed Code',
                        onAction: function () {
                            //// set
                            //elFinder.prototype.commands.addtoEditor = function () {
                            //    this.exec = function (hashes) {
                            //        selectedFiles(hashes, editor);
                            //    }
                            //    this.getstate = function () {
                            //        return 0;
                            //    }
                            //};
                            ///selectedFiles(editor);
                            $('' + eleElfinder + '-modal').modal('show');
                        }
                    });
            }
        });
    }
}  