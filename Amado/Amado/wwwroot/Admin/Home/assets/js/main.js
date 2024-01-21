let previewContainer = document.getElementById('imagePreview');
let filesInput = document.getElementById('imageInput');
let priceInput = document.getElementById('price');

filesInput.addEventListener('change', () => {
    
    for (var i = 0; i < filesInput.files.length; i++) {
        var file = filesInput.files[i];
        var reader = new FileReader();

        reader.onload = (function (e) {
            var imgElement = document.createElement('img');
            imgElement.src = e.target.result;
            imgElement.style.maxWidth = '150px';
            imgElement.style.maxHeight = '150px';

            var imageContainer = document.createElement('div');
            imageContainer.classList.add('image-container');

            imageContainer.appendChild(imgElement);

            var deleteIcon = document.createElement('span');
            deleteIcon.classList.add('delete-icon');
            deleteIcon.textContent = 'X';
            deleteIcon.onclick = function () {
                imageContainer.remove();
            };

            imageContainer.appendChild(deleteIcon);
            previewContainer.appendChild(imageContainer);
        });

        reader.readAsDataURL(file);
    }
});


previewContainer.addEventListener('click', function (e) {
    if (e.target.classList.contains('delete-icon')) {
        e.target.parentNode.remove();
        filesInput.value = '';
    }
});

priceInput.addEventListener('keyup', (e) => {
    let inputValue = e.target.value;
    
    let formattedValue = inputValue.replace('.', ',');

    e.target.value = formattedValue;
});