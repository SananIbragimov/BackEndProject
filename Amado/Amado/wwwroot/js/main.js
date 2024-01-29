$(function () {
    var slider = $(".slider-range-price");

    slider.slider({
        range: true,
        min: parseInt(slider.attr("data-min")),
        max: parseInt(slider.attr("data-max")),
        values: [parseInt(slider.attr("data-value-min")), parseInt(slider.attr("data-value-max"))],
        slide: function (event, ui) {
            $("#minPrice").text(ui.values[0]);
            $("#maxPrice").text(ui.values[1]);
            $("#hiddenMinPrice").val(ui.values[0]);
            $("#hiddenMaxPrice").val(ui.values[1]);
        }
    });
});

{
    let searchInput = document.querySelector(".search-input");
    let searchResult = document.querySelector(".search-result");
    let cachedSearchValue;

    searchInput.addEventListener('keyup', (e) => {
        let searchValue = e.target.value.trim()
        if (searchValue.length < 3) {
            cachedSearchValue = null
            searchResult.innerHTML = ''
            return
        }
        if (searchValue == cachedSearchValue) return
        cachedSearchValue = searchValue
        renderSearchResult(searchValue)
    })

    function renderSearchResult(value) {
        fetch(`https://localhost:7271/shop/search?input=${value}`)
            .then(x => x.text())
            .then(x => {
                searchResult.innerHTML = '';
                searchResult.innerHTML += x;
            })
            .catch(error => {
                console.error('Error:', error);
            });

    }
}