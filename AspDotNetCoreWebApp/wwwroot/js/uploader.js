(function () {
    var dropTarget = $('#fileDrop');
    dropTarget.on('dragover', function (e) {
        console.log('dragover');
        e.stopPropagation();
        e.preventDefault();
    });
    dropTarget.on('drop', function (e) {
        console.log('drop');
        e.stopPropagation();
        e.preventDefault();
        var data = e.originalEvent.dataTransfer;

        handleFile(data.files[0]);
    });

    function handleFile(file) {
        var reader = new FileReader();
        reader.onload = function (e2) {
            var ajaxparam = {
                url: url + '/' + file.name + sas,
                type: 'PUT',
                data: new Uint8Array(e2.target.result),
                contentType: file.type,
                headers: { 'x-ms-blob-type': 'BlockBlob' },
                processData: false,
                complete: function (xhr, status) { alert(status); },
                error: function (xhr, status, err) { alert(err); }
            };

            console.log(ajaxparam.url);
            $.ajax(ajaxparam);
        };
        reader.readAsArrayBuffer(file);
    }

})();
