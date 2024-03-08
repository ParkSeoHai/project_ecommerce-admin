
let productColors = [];
let productOptions = [];
let productImages = [];
let productAddressShops = [];
let productProperties = [];
let stepActive = 1;
const productId = crypto.randomUUID();
let productName, productPrice, productDiscount;

let productImageActive = '';

// Format number to VND
function formatToVND(number) {
    const numberFormat = new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND',
    }).format(number);
    return numberFormat;
}

// Display images
function displayProductImage(productImages) {
    const productImageElement = document.querySelector('.product-info__image');
    productImageElement.innerHTML = '';

    if (productImageActive === '') productImageActive = productImages[0].id;

    productImages.forEach(image => {
        const productImageItem = `
            <div class="product-info__image--item ${productImageActive == image.id ? 'active' : ''}"
                onclick="setImageActive('${image.id}')">
                <img src="${image.src}"
                     alt="">
            </div>
        `;

        productImageElement.innerHTML += productImageItem;
    })
}

function setImageActive(imageId) {
    productImageActive = imageId;
    displayProductImage(productImages);
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
                <td>${formatToVND(color.price)}</td>
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
    const priceColor = document.getElementById('price-color').value;
    const quantityColor = document.getElementById('quantity-color').value;

    if (nameColor.trim() !== '' && priceColor.trim() !== '' && quantityColor.trim() !== '') {
        const color = {
            // id: productColors.length > 0 ? productColors[productColors.length - 1].id + 1 : 1,
            id: crypto.randomUUID(),
            name: nameColor,
            price: priceColor,
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
                <td>${formatToVND(option.price)}</td>
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
    const priceOption = document.getElementById('price-option').value;
    const quantityOption = document.getElementById('quantity-option').value;
    const colorIdOption = document.getElementById('product-select__color--option').value;

    if (valueOption.trim() !== '' && priceOption.trim() !== ''
        && quantityOption.trim() !== '' && colorIdOption !== '0') {
        const option = {
            // id: productOptions.length > 0 ? productOptions[productOptions.length - 1].id + 1: 1,
            id: crypto.randomUUID(),
            name: typeOption,   // or type
            value: valueOption,
            price: priceOption,
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

// Product property
function displayProperties(productProperties) {
    const tableTbodyElement = document.querySelector('.product-info__property--table tbody');
    if (tableTbodyElement) {
        tableTbodyElement.innerHTML = '';

        let index = 1;
        productProperties.forEach(property => {
            const trElement = `
                <tr>
                    <th scope="row">${index}</th>
                    <td>${property.name}</td>
                    <td>${property.value}</td>
                    <td>
                        <span class="text-danger fw-semibold" onclick="removeProperty('${property.id}')" style="cursor: pointer;">Delete</span>
                    </td>
                </tr>
            `;

            index++;
            tableTbodyElement.innerHTML += trElement;
        });
    }
}

function addProperty() {
    const propertyName = document.getElementById('property-name').value;
    const propertyValue = document.getElementById('property-value').value;

    productProperties.push({
        id: crypto.randomUUID(),
        name: propertyName,
        value: propertyValue,
        productId: productId
    });
    // Display table
    displayProperties(productProperties);
}

function removeProperty(propertyId) {
    let propertiesNew = [];

    productProperties.forEach(property => {
        if (property.id !== propertyId) {
            propertiesNew.push(property);
        }
    });
    productProperties = propertiesNew;
    // Display table
    displayProperties(productProperties);
}

// Product preview
function displayProductPreview() {
    const previewElement = document.querySelector('.product-info__preview');
    productName = document.getElementById('product-name').value;
    productPrice = document.getElementById('product-price').value;

    // Calc product price sale and format
    productDiscount = document.getElementById('product-discount').value;
    const productPriceSale = productPrice - (productPrice * (productDiscount / 100));

    // Get src image default
    let srcDefaultImage;
    productImages.forEach(image => {
        if (image.id === productImageActive) {
            srcDefaultImage = image.src;
        }
    });

    if (productImages.length > 0 && productName && productColors.length > 0 && productPrice) {
        const html = `
            <div class="product-info__preview--img">
                <img src="${srcDefaultImage}"
                     alt="${productName}"
                     class="w-100 h-100 object-fit-cover">
            </div>
            <div class="product-info__preview--content mt-3">
                <h3 class="product-info__preview--name">${productName}</h3>
                <div>
                    <ul class="list-variants pt-4">
                        <li>+${productColors.length} màu sắc</li>
                    </ul>
                </div>
                <div class="box-pro-prices pt-2 d-flex align-items-center gap-4">
                    <span class="text-danger fw-bold">${formatToVND(productPriceSale)}</span>
                    <del class="compare-price d-block">${formatToVND(productPrice)}</del>
                </div>
            </div>
        `;
        previewElement.innerHTML = html;
    }
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
    productPrice = document.getElementById('product-price').value;
    productDiscount = document.getElementById('product-discount').value;
    const productVisibility = document.querySelector('input[name="product-visibility"]:checked').value;
    let isPublish = productVisibility === 'publish' ? true : false;
    // Product quantity calc from quantity each product color
    let productQuantity = 0;
    productColors.forEach(color => {
        productQuantity += Number.parseInt(color.quantity);
    })
    // Get image default
    let imageDefaultSrc = '';
    productImages.forEach(image => {
        if (image.id === productImageActive) {
            imageDefaultSrc = image.src;
        }
    });

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
        defaultImage: imageDefaultSrc,
        colors: productColors,
        options: productOptions,
        productShops: productAddressShops,
        properties: productProperties
    }
}

// Call function
displayProductColor(productColors);
displayProductOptionTable(productOptions);
showStepBodyActive(stepActive);