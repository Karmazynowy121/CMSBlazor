function toggleTableColapse (elementName) {
  const elements = document.getElementsByName(elementName);
  if (elements) {
    elements.forEach(e => {
      e.classList.toggle('show');
    })
  }
}

function runEditor() {
  ClassicEditor
    .create( document.querySelector( '#editor' ) )
    .catch( error => {
        console.error( error );
    } );
}

function js_upload_handler(blobInfo, success, failure, progress) {
console.log(blobInfo.filename());
window.dotNetHelper.invokeMethodAsync('UploadHandler', blobInfo.base64(), blobInfo.filename())
  .then((data) => {
      success(data);
  });
}