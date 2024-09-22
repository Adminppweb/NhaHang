
(function () {
    angular
        .module('simplAdmin.common')
        .factory('tinymceConfigService', tinymceConfigService);

    /* @ngInject */
    function tinymceConfigService(fileUploadService) {
        var defaultConfig = {
            language: 'vi',
            language_url: '/lib/tinymce/langs5/vi.js',
            menubar: true,
            branding: false,
            toolbar: [
                "undo redo cut copy paste blocks styleselect fontselect fontsizeselect bold italic underline strikethrough unlink anchor align lineheight checklist forecolor backcolor bullist numlist outdent indent blockquote subscript superscript advlist lists autolink table emoticons charmap link image media print preview code removeformat fullscreen"
            ],
           
            plugins: 'anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount code preview fullscreen autosave save directionality visualchars pagebreak nonbreaking insertdatetime advlist help quickbars accordion',
            max_height: 700,
            min_height: 700,
            automatic_uploads: true,
            images_upload_url: '/api/pages/upload-image',
            file_picker_types: 'image',
            images_upload_handler: function (blobInfo, success, failure) {
                var file = blobInfo.blob();

                fileUploadService.uploadFile(file, '/api/pages/upload-image')
                    .then(function (response) {
                        if (response.data && response.data.url) {
                            success(response.data.url);
                        } else {
                            failure('Upload failed');
                        }
                    })
                    .catch(function () {
                        failure('Upload failed');
                    });
            },
            file_picker_callback: function (callback, value, meta) {
                tinymce.activeEditor.windowManager.openUrl({
                    url: '/tiny-mce/browse',
                    title: 'File Manager',
                    width: 1600,
                    height: 900,
                    resizable: 'yes',
                    onMessage: function (api, data) {
                        if (data.mceAction.file) {
                            var file = data.mceAction.file;
                            callback(file.url, { alt: file.name });
                            api.close();
                        }
                    }
                });
            }
        };

        return {
            getDefaultConfig: function () {
                return angular.copy(defaultConfig);
            }
        };
    }
})();