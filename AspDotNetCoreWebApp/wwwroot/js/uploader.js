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
        debugger;
    }


})();
