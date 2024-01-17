// Resim önizleme alanını temsil eden bir HTML öğesi
let previewContainer = document.getElementById('imagePreview');
// Seçilen dosyaların listesi
let filesInput = document.getElementById('imageInput');

filesInput.addEventListener('change', () => {
    // Her bir dosya için önizleme ekle
    for (var i = 0; i < filesInput.files.length; i++) {
        var file = filesInput.files[i];
        var reader = new FileReader();

        // Dosya okunduğunda tetiklenecek olay
        reader.onload = (function (e) {
            // Dosya içeriğini temsil eden bir img HTML öğesi
            var imgElement = document.createElement('img');
            imgElement.src = e.target.result; // Dosya içeriği
            imgElement.style.maxWidth = '150px'; // Max genişlik
            imgElement.style.maxHeight = '150px'; // Max yükseklik

            // Resim önizleme alanına ekle
            var imageContainer = document.createElement('div');
            imageContainer.classList.add('image-container');

            imageContainer.appendChild(imgElement);

            // Silme ikonu oluştur
            var deleteIcon = document.createElement('span');
            deleteIcon.classList.add('delete-icon');
            deleteIcon.textContent = 'X';
            deleteIcon.onclick = function () {
                // Resmi önizleme alanından kaldır
                imageContainer.remove();
                // İlgili resmin adını al
                var imageUrl = imgElement.src;
                var fileName = imageUrl.substring(imageUrl.lastIndexOf('/') + 1);
                // Silinecek resmi diziye ekle
                deletedImages.push(fileName);
            };

            // Resim container'a silme ikonunu ekle
            imageContainer.appendChild(deleteIcon);

            // Resim önizleme alanına ekle
            previewContainer.appendChild(imageContainer);
        });

        // Dosyayı oku
        reader.readAsDataURL(file);
    }
});
