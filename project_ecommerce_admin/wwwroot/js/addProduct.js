
let productColors = [];
let productOptions = [];
let productImages = [];
let productAddressShops = [];
let stepActive = 1;
const productId = crypto.randomUUID();
let productName;

// Display images
function displayProductImage(productImages) {
    const productImageElement = document.querySelector('.product-info__image');
    productImageElement.innerHTML = '';

    productImages.forEach(image => {
        const productImageItem = `
            <div class="product-info__image--item">
                <img src="${image.src}"
                     alt="">
            </div>
        `;

        productImageElement.innerHTML += productImageItem;
    })
}

// Images upload to cloudinary
function uploadFile(file, folderName) {
    const cloudName = 'dvtvl2un6';
    const unsignedUploadPreset = 'sqsfjomd';
    const url = `https://api.cloudinary.com/v1_1/${cloudName}/upload`;

    const fd = new FormData();
    fd.append('folder', folderName);
    fd.append('upload_preset', unsignedUploadPreset);
    fd.append('tags', 'browser_upload'); // Optional - add tags for image admin in Cloudinary
    fd.append('file', file);

    fetch(url, {
        method: 'POST',
        body: fd, // Your form data containing the file(s)
        headers: {
            'X-Requested-With': 'XMLHttpRequest',
        },
    })
        .then((response) => response.json())
        .then((data) => {
            productImages.push(
                {
                    id: crypto.randomUUID(),
                    src: data.url,
                    productId: productId
                }
            )
            displayProductImage(productImages);
        })
        .catch((error) => {
            console.error('Error uploading the file:', error);
        });
}

// Hanlde when click button upload
function uploadProductImage() {
    productName = document.getElementById('product-name').value;
    if (productName.trim() === '') {
        alert('Please enter product name');
        return;
    }

    const fileInput = document.getElementById("product-images");
    const files = fileInput.files; // Get the selected file(s)
    const folder = `project-ecommerce/product/${productName}`;

    // Check if any file is selected
    if (files.length === 0) {
        console.error('No file selected.');
        return;
    }

    // *********** Handle selected files *********** //
    for (let i = 0; i < files.length; i++) {
        uploadFile(files[i], folder); // call the function to upload the file
    }
}

// Colors
function displayProductColor(productColors) {
    const tableColorElement = document.querySelector('.product-info__color--table tbody');
    tableColorElement.innerHTML = '';
    let count = 1;
    productColors.forEach(color => {
        const html = `
                        <tr>
                            <th scope="row">${count}</th>
                            <td>${color.name}</td>
                            <td>${color.pricePlus}</td>
                            <td>${color.quantity}</td>
                            <td>
                                <span class="text-danger fw-semibold" onclick="removeColor('${color.id}')" style="cursor: pointer;">Delete</span>
                            </td>
                        </tr>
                    `;
        ++count;
        tableColorElement.innerHTML += html;
    });
    displayProductOptionColorSelect();
}

function addColor() {
    const nameColor = document.getElementById('name-color').value;
    const pricePlusColor = document.getElementById('price-plus-color').value;
    const quantityColor = document.getElementById('quantity-color').value;

    if (nameColor.trim() !== '' && pricePlusColor.trim() !== '' && quantityColor.trim() !== '') {
        const color = {
            // id: productColors.length > 0 ? productColors[productColors.length - 1].id + 1 : 1,
            id: crypto.randomUUID(),
            name: nameColor,
            pricePlus: pricePlusColor,
            quantity: quantityColor,
            productId: productId
        }

        productColors.push(color);
        displayProductColor(productColors);
        updateProductAddressColorSelect(productColors);
    }
}

function removeColor(id) {
    let newProductColors = [];
    let newProductOptions = [];

    productColors.forEach(color => {
        if (color.id !== id) {
            newProductColors.push(color);
        }
    })

    productOptions.forEach(option => {
        if (option.color !== id) {
            newProductOptions.push(option);
        }
    })

    productColors = newProductColors;
    productOptions = newProductOptions;
    displayProductColor(productColors);
    displayProductOptionTable(productOptions);
}

// Options
function displayProductOptionColorSelect() {
    const selectElement = document.querySelector('#product-select__color--option');
    selectElement.innerHTML = '';
    selectElement.innerHTML += '<option value="0">Color</option>';

    productColors.forEach(color => {
        const html = `
                        <option value="${color.id}">${color.name}</option>
                    `;
        selectElement.innerHTML += html;
    })
}

function displayProductOptionTable(productOptions) {
    const tableOptionElement = document.querySelector('.product-info__option--table tbody');
    tableOptionElement.innerHTML = '';
    productOptions.forEach(option => {
        let colorName = '';
        productColors.forEach(color => {
            if (color.id == option.colorId) {
                colorName = color.name;
            }
        })

        const html = `
                        <tr>
                            <th scope="row">${option.name}</th>
                            <td>${option.value}</td>
                            <td>${option.pricePlus}</td>
                            <td>${option.quantity}</td>
                            <td>${colorName}</td>
                            <td>
                                <span class="text-danger fw-semibold" onclick="removeOption('${option.id}')" style="cursor: pointer;">Delete</span>
                            </td>
                        </tr>
                    `;
        tableOptionElement.innerHTML += html;
    });
}

function addOption() {
    const typeOption = document.getElementById('product-info__option--type').value;
    const valueOption = document.getElementById('option-value').value;
    const pricePlusOption = document.getElementById('price-plus-option').value;
    const quantityOption = document.getElementById('quantity-option').value;
    const colorIdOption = document.getElementById('product-select__color--option').value;

    if (valueOption.trim() !== '' && pricePlusOption.trim() !== ''
        && quantityOption.trim() !== '' && colorIdOption !== '0') {
        const option = {
            // id: productOptions.length > 0 ? productOptions[productOptions.length - 1].id + 1: 1,
            id: crypto.randomUUID(),
            name: typeOption,   // or type
            value: valueOption,
            pricePlus: pricePlusOption,
            quantity: quantityOption,
            colorId: colorIdOption
        }

        productOptions.push(option);
        displayProductOptionTable(productOptions);
    }
}

function removeOption(id) {
    let newProductOptions = [];

    productOptions.forEach(option => {
        if (option.id !== id) {
            newProductOptions.push(option);
        }
    })

    productOptions = newProductOptions;
    displayProductOptionTable(productOptions);
}

// Product address
function displayProductAddressTable(productAddressShops) {
    const tableElement = document.querySelector('.product-info__address--table tbody');
    tableElement.innerHTML = '';

    if (productAddressShops.length > 0) {
        productAddressShops.forEach(productAddress => {
            const html = `
                        <tr>
                            <th scope="row">${productAddress.colorName}</th>
                            <td>${productAddress.type}</td>
                            <td>${productAddress.value}</td>
                            <td>${productAddress.quantity}</td>
                            <td>${productAddress.address}</td>
                            <td>
                                <span class="text-danger fw-semibold" onclick="removeProductAddress('${productAddress.id}')" style="cursor: pointer;">Delete</span>
                            </td>
                        </tr>
                    `;
            tableElement.innerHTML += html;
        });
    }
}

function updateProductAddressColorSelect(productColors) {
    const selectElement = document.getElementById('product-info__address--color');
    selectElement.innerHTML = '';
    selectElement.innerHTML = '<option selected value="0">Select color</option>';

    if (productColors.length > 0) {
        productColors.forEach(color => {
            const html = `
                <option value="${color.id}">${color.name}</option>
            `;
            selectElement.innerHTML += html;
        });
    }
}

function onChangeProductAddressColorSelect(selectId, e) {
    const colorId = e.value;

    const selectElement = document.getElementById(selectId);
    selectElement.innerHTML = '';
    selectElement.innerHTML = '<option selected value="0">Select option</option>';

    // Get option by color id
    if (productOptions.length > 0) { 
        productOptions.forEach(option => {
            if (option.colorId === colorId) {
                selectElement.innerHTML += `
                    <option value="${option.id}">${option.value}</option>
                `;
            }
        })
    }
}

function addProductAddress() {
    const optionId = document.getElementById('product-info__address--option').value;
    const addressShop = document.getElementById('product-info__address--shop');
    const quantity = document.getElementById('product-info__address--quantity').value;

    if (optionId !== '0' && addressShop.value !== '0' && quantity.trim() !== '') {
        // Get color name by color id
        let colorName = '';
        const colorId = document.getElementById('product-info__address--color').value;
        productColors.forEach(color => {
            if (color.id === colorId) {
                colorName = color.name;
            }
        })
        // Get type, value option by option id
        let typeOption = '';
        let valueOption = '';
        productOptions.forEach(option => {
            if (option.id === optionId) {
                typeOption = option.name;
                valueOption = option.value;
            }
        })
        // Get text address
        const addressText = addressShop.options[addressShop.selectedIndex].text;

        const productAddress = {
            id: crypto.randomUUID(),
            optionId: optionId,
            addressShopId: addressShop.value,
            quantity: quantity,
            colorName: colorName,
            type: typeOption,
            value: valueOption,
            address: addressText
        }

        productAddressShops.push(productAddress);
        displayProductAddressTable(productAddressShops);
    }
}

function removeProductAddress(productAddressId) {
    let productAddressShopsNew = [];
    productAddressShops.forEach(productAddress => {
        if (productAddress.id !== productAddressId) {
            productAddressShopsNew.push(productAddress)
        }
    })

    productAddressShops = productAddressShopsNew;
    displayProductAddressTable(productAddressShops);
}

// Handle show / hidden step
function showStepBodyActive(stepActive) {
    // Remove all class show step body
    const stepBodyList = document.querySelectorAll('.product-info__step--body');
    stepBodyList.forEach(stepBody => {
        stepBody.classList.remove('show');
    })

    // Remove all class active step header
    const stepHeaderList = document.querySelectorAll('.product-info__step--item');
    stepHeaderList.forEach(stepHeader => {
        stepHeader.classList.remove('active');
    })

    // Show step active header
    const stepHeaderElement = document.querySelector(`.product-step__header--${stepActive}`);
    if (stepHeaderElement) {
        stepHeaderElement.classList.add('active');
    }

    // Show step active body
    const stepBodyElement = document.querySelector(`.product-step__body--${stepActive}`);
    if (stepBodyElement) {
        stepBodyElement.classList.add('show');
    }
}

function showStep(stepNumber) {
    stepActive = stepNumber;
    showStepBodyActive(stepActive);
}

// Get info product
function getInfoProduct() {
    const brandId = document.getElementById('product-brand').value;
    const categoryId = document.getElementById('product-category__3').value;
    productName = document.getElementById('product-name').value;
    const productDescription = document.getElementById('product-description').value;
    const productPrice = document.getElementById('product-price').value;
    const productDiscount = document.getElementById('product-discount').value;
    const productVisibility = document.querySelector('input[name="product-visibility"]:checked').value;
    let isPublish = productVisibility === 'publish' ? true : false;

    let productQuantity = 0;
    productColors.forEach(color => {
        productQuantity += Number.parseInt(color.quantity);
    })

    return productInfo = {
        id: productId,
        categoryId: categoryId,
        brandId: brandId,
        name: productName,
        description: productDescription,
        price: productPrice,
        discount: productDiscount,
        quantity: productQuantity,
        publish: isPublish,
        images: productImages,
        colors: productColors,
        options: productOptions,
        productShops: productAddressShops
    }
}

// Call function
displayProductColor(productColors);
displayProductOptionTable(productOptions);
showStepBodyActive(stepActive);