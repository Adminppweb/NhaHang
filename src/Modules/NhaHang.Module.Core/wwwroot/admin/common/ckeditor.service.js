angular.module('simplAdmin.ckeditor', [])
    .directive('ckeditor', ['$timeout', function ($timeout) {
        return {
            require: '?ngModel',
            link: function (scope, element, attr, ngModel) {
                if (!ngModel) return;

                ClassicEditor
                    .create(element[0], {
                        toolbar: [
                            'undo', 'redo',
                            '|', 'sourceEditing', 'showBlocks', 'findAndReplace', 'selectAll', 'textPartLanguage',
                            '|', 'heading', 'style', 'fontFamily', 'fontSize', 'fontColor', 'fontBackgroundColor', 'bold', 'italic', 'link',
                            '|', 'underline', 'strikethrough', 'subscript', 'superscript', 'code', 'removeFormat',
                            '|', 'specialCharacters', 'horizontalLine', 'pageBreak', 'insertImage', 'mediaEmbed', 'insertTable',
                            '|', 'alignment', 'highlight', 'blockQuote', 'codeBlock', 'htmlEmbed',
                            '|', 'bulletedList', 'numberedList', 'todoList', 'outdent', 'indent',
                            '|', 'accessibilityHelp'
                        ],
                        heading: {
                            options: [
                                { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
                                { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
                                { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' },
                                { model: 'heading3', view: 'h3', title: 'Heading 3', class: 'ck-heading_heading3' },
                                { model: 'heading4', view: 'h4', title: 'Heading 4', class: 'ck-heading_heading4' },
                                { model: 'heading5', view: 'h5', title: 'Heading 5', class: 'ck-heading_heading5' },
                                { model: 'heading6', view: 'h6', title: 'Heading 6', class: 'ck-heading_heading6' }
                            ]
                        },
                        fontFamily: {
                            options: [
                                'default',
                                'Arial, Helvetica, sans-serif',
                                'Courier New, Courier, monospace',
                                'Georgia, serif',
                                'Lucida Sans Unicode, Lucida Grande, sans-serif',
                                'Tahoma, Geneva, sans-serif',
                                'Times New Roman, Times, serif',
                                'Trebuchet MS, Helvetica, sans-serif',
                                'Verdana, Geneva, sans-serif'
                            ]
                        },
                        fontSize: {
                            options: [
                                9,
                                11,
                                13,
                                'default',
                                17,
                                19,
                                21
                            ],
                            supportAllValues: true
                        },
                        fontColor: {
                            columns: 5,
                            documentColors: 10
                        },
                        fontBackgroundColor: {
                            columns: 5,
                            documentColors: 10
                        },
                        image: {
                            toolbar: [
                                'imageTextAlternative',
                                'toggleImageCaption',
                                'imageStyle:inline',
                                'imageStyle:block',
                                'imageStyle:side'
                            ]
                        },
                        extraPlugins: [MyCustomUploadAdapterPlugin]
                    })
                    .then(editor => {
                        editor.model.document.on('change:data', () => {
                            $timeout(() => {
                                ngModel.$setViewValue(editor.getData());
                            });
                        });

                        ngModel.$render = () => {
                            editor.setData(ngModel.$modelValue || '');
                        };

                        scope.$on('$destroy', () => {
                            editor.destroy();
                        });
                    })
                    .catch(error => {
                        console.error(error);
                    });
            }
        };
    }]);

function MyCustomUploadAdapterPlugin(editor) {
    editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
        return new MyUploadAdapter(loader);
    };
}

class MyUploadAdapter {
    constructor(loader) {
        this.loader = loader;
        this.url = '/api/pages/upload-image';
    }

    upload() {
        return this.loader.file
            .then(file => new Promise((resolve, reject) => {
                const data = new FormData();
                data.append('file', file);

                fetch(this.url, {
                    method: 'POST',
                    body: data,
                })
                    .then(response => response.json())
                    .then(result => {
                        resolve({
                            default: result.url
                        });
                    })
                    .catch(error => {
                        reject(error);
                    });
            }));
    }

    abort() {
        // Implement abort functionality if needed
    }
}


