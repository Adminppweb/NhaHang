(function () {
    //// testTool
    elFinder.prototype._options.commands.push('customFunction');
    function customFunction() {
        alert('Custom Function Called');
    }
    let myCommands = elFinder.prototype._options.commands;
    elFinder.prototype.i18.en.messages.TextArea = "Edit";
    elFinder.prototype.i18.en.messages.errNewNameSelection = 'Unable to create new file with name "$1"';
    elFinder.uploadURL = "/el-finder/file-system/UploadFile";
    //elFinder.prototype._options.commandsOptions.customFunction = {
    //    exec: customFunction,
    //    title: 'Custom Function',
    //    ui: 'button'
    //};

    ////// uiOptions
    // Định nghĩa công cụ tùy chỉnh bạn muốn thêm
    //elFinder.prototype._options.commands.push('editimage');
    //elFinder.prototype.commands.editimage = function () {
    //    this.exec = function (hashes) {
    //        alert('editimage');
    //    }
    //    this.getstate = function () {
    //        return 0;
    //    }
    //}

    ///elFinder.uploadURL = "/el-finder/file-system/UploadFile";

    ////let toolbar = [
    ////    ['home'],
    ////    ['upload', 'save'],
    ////    /*['editimage'],*/
    ////    ['back', 'forward'],
    ////    ['reload'],
    ////    ['mkdir', 'mkfile'],
    ////    ['open', 'download'],
    ////    ['undo', 'redo'],
    ////    ['info'],
    ////    ['quicklook'],
    ////    ['copy', 'cut', 'paste'],
    ////    ['rm'],
    ////    ['duplicate', 'rename', 'edit'],
    ////    ['selectall', 'selectnone', 'selectinvert'],
    ////    ['view', 'sort'],
    ////    ['search'],

    ////];
    ////let options = {
    ////    debug: false,
    ////    url: '/el-finder/file-system/connector',
    ////    //// commands: myCommands,
    ////    uiOptions: {
    ////        toolbar: toolbar
    ////    }
    ////};

    let toolbar = [
        ['home'],
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
        url: '/el-finder/file-system/connector', // Default (Local File System)
        ////customData: { folder: '@@Model.Folder', subFolder: '@@Model.SubFolder' }, // customData passed in every request to the connector as query strings. These values are used in FileController's Index method.
        rememberLastDir: true, // Prevent elFinder saving in the Browser LocalStorage the last visited directory
        customData: {
            // Include any additional data needed for authentication or other purposes
        },
        commands: myCommands,
        //lang: 'pt_BR', // elFinder supports UI and messages localization. Check the folder Content\elfinder\js\i18n for all available languages. Be sure to include the corresponding .js file(s) in the JavaScript bundle.
        uiOptions: { // UI buttons available to the user
            toolbar: toolbar
        },
        //onlyMimes: ["image", "text/plain"] // Get files of requested mime types only
        //lang: 'vi',
        handlers: {
            upload: function (event, elfinderInstance) {
                elfinderInstance.ajax({
                    ////url: 'UploadHandler.ashx',
                    url: 'el-finder/file-system/UploadFile',
                    data: event.data,
                    type: 'POST',
                    cache: false,
                    contentType: false,
                    processData: false,
                    xhrFields: { withCredentials: true }, // Include if using credentials
                    beforeSend: function (xhr) {
                        // Include any headers or additional setup before sending the request
                        console.log(xhr);
                    },
                    success: function (data) {
                        // Handle the success response from the server
                        console.log('Upload successful', data);
                    },
                    error: function (error) {
                        // Handle any errors that occur during the upload
                        console.error('Error uploading file', error);
                    }
                });
            }
        }
    };

    Init();
    function Init() {
        ////elFinder.uploadURL = "/el-finder/file-system/UploadFile";
        console.log(elFinder);

        $('#elfinder-manager-file').elfinder(options);
    }
})();