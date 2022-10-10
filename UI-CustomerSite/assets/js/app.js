// Material function
function debounceFn(func, wait, immediate) {
    let timeout;
    return function () {
        let context = this,
            args = arguments;
        let later = function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };
        let callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow) func.apply(context, args);
    };
}

const checkMoblie = window.mobileCheck = function () {
    let check = false;
    (function (a) {
        if (
            /(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(
                a
            ) ||
            /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(
                a.substr(0, 4)
            )
        )
            check = true;
    })(navigator.userAgent || navigator.vendor || window.opera);
    return check;
};


/*==================== REMOVE PRELOADER ====================*/
// window.addEventListener("load", () => {
//     document.body.classList.remove("unable-scroll");
//     document.querySelector(".preloader").style.display = "none";
// })
window.addEventListener("load", function () {
    $(".preloader").fadeOut('slow');
    document.body.classList.remove("unable-scroll");
});

/*==================== Initialize Swiper LIBRARY ====================*/
var swiper = new Swiper(".mySwiper", {
    loop: true,
    autoplay: {
        delay: 2500,
        disableOnInteraction: false,
    },
    pagination: {
        el: ".swiper-pagination",
    },
});

/*==================== Initialize SCROLLREVEAL LIBRARY ====================*/
ScrollReveal().reveal('.section');
ScrollReveal().reveal('.intro-section');
ScrollReveal().reveal('.product-section');

/*==================== FIXED HEADER BRAND / SHOW BUTTON BACK TO TOP ====================*/
const headerBrandScroll = this.document.querySelector(".header-brand-scroll");
const btnBackToTop = document.querySelector(".btn-backToTop");

window.addEventListener("scroll", debounceFn(function (e) {
    const scrollY = window.pageYOffset;
    if (scrollY >= headerBrandScroll.offsetHeight) {
        headerBrandScroll && headerBrandScroll.classList.add("is-fixed"); // if(header) header.classList.add("is-fixed");
        btnBackToTop && btnBackToTop.classList.add("is-show");
    } else {
        headerBrandScroll && headerBrandScroll.classList.remove("is-fixed"); // if(header) header.classList.remove("is-fixed");
        btnBackToTop && btnBackToTop.classList.remove("is-show");
    }
}, 10)
);

/*==================== HANDLE CLICK BUTTON BACK TO TOP ====================*/
function scrollToTop() {
    window.scrollTo({
        top: 0,
        behavior: "smooth"
    })
}

/*==================== OPEN MENU NAVIGATION MOBILE ====================*/
const menuBtn = document.querySelector('.menu-btn');
const menu = document.querySelector('.navigation__menu');
const overlay = document.querySelector('.overlay');
menuBtn.addEventListener('click', () => {
    menuBtn.classList.toggle("open");
    menu.classList.toggle("is-show");


    // Get height of Header Element
    const heightHeader = document.querySelector("header").offsetHeight;
    menu.style['top'] = heightHeader + 'px';

    // Unable scroll
    document.body.classList.toggle("unable-scroll");
});


/*==================== MENU NAVIGATION ACCORDION MOBILE ====================*/
const navWraps = document.querySelectorAll(".nav__item-wrap-mobile");
navWraps.forEach(item => item.addEventListener("click", (e) => {

    const megamenu = e.currentTarget.nextElementSibling;
    if (megamenu) {

        megamenu.classList.toggle("is-active");

        // Change icon
        const icon = e.currentTarget.querySelector("i");
        icon.classList.toggle("uil-plus")
        icon.classList.toggle("uil-minus")
    }
}))



/*==================== CHANGE LOCATION FORM SEARCH IN NAVIGATION MENU MOBILE ====================*/
if (checkMoblie()) {
    const wrapFormSearchSwap = document.querySelector(".wrap-search-mobile");
    const formSearch = document.querySelector("#form-search");
    formSearch.classList.remove("d-none");

    wrapFormSearchSwap.parentNode.insertBefore(formSearch, wrapFormSearchSwap);

}

/*==================== HANDLE CLICK BUTTON ADD WISHLIST ====================*/
function AddWishList(event) {
    event.target.querySelector("i").classList.toggle("fa-solid")
    toastr.success('Wishlist add successfully')
}


/*==================== ALL EVENT FUNCTION IN PRODUCT LIST PAGE ====================*/
function HandleShowFilterArea(event) {
    const filterArea = document.querySelector(".filtering-area");
    const container = document.querySelector(".product-list-section");

    filterArea && filterArea.classList.add("is-show");
    container && container.classList.add("clear-mp");
}
function HandleCloseFilterArea(event) {
    const filterArea = document.querySelector(".filtering-area");
    const container = document.querySelector(".product-list-section");

    filterArea && filterArea.classList.remove("is-show");
    container && container.classList.remove("clear-mp");
}

function HandleToggleCatList(event) {
    const filterCatList = event.currentTarget.parentNode.querySelector(".filter-cat-list");
    const icon = event.currentTarget.querySelector("i");
    icon.classList.toggle("fa-plus");
    icon.classList.toggle("fa-minus");
    filterCatList && filterCatList.classList.toggle("is-show");
}


const sizeItems = document.querySelectorAll(".size-items");
sizeItems.forEach(item => item.addEventListener("click", function (e) {
    e.target.classList.add("active");
}))


/*==================== THUMBNAIL PRODUCT SLIDER ====================*/
let wrapSliderProduct = document.querySelector('.wrap-slider-product');

if (wrapSliderProduct) {
    let listDivImg = document.querySelectorAll('.list-img div')
    let btnNextSlider = document.querySelector('.next')
    let btnPrevSlider = document.querySelector('.prev')
    let imgWrap = document.querySelector('.img-wrap img')

    let currentIndex = 0;

    setCurrent(currentIndex)

    function setCurrent(index) {
        currentIndex = index
        imgWrap.src = listDivImg[currentIndex].querySelector('img').src

        // remove all active img
        listDivImg.forEach((item) => {
            item.classList.remove('active')
        })

        // set active
        listDivImg[currentIndex].classList.add('active')
    }

    listDivImg.forEach((img, index) => {
        img.addEventListener('click', (e) => {
            setCurrent(index)
        })
    })


    btnNextSlider && btnNextSlider.addEventListener('click', () => {
        if (currentIndex == listDivImg.length - 1) {
            currentIndex = 0
        } else currentIndex++

        setCurrent(currentIndex)
    })

    btnPrevSlider && btnPrevSlider.addEventListener('click', () => {
        if (currentIndex == 0) currentIndex = listDivImg.length - 1
        else currentIndex--

        setCurrent(currentIndex)
    })
}

/*==================== SHOW LIGHT BOX (ZOOM IN IMAGE PRODUCT) ====================*/
function HandleShowLightbox(event) {
    const imageSrc = event.target.getAttribute("src");
    const templateLightbox = `       
        <div class="lightbox" onclick="HandleHideLightbox(event)">
            <div class="lightbox-content">
                <img src= "${imageSrc}"  alt="" class="img-fluid lightbox-image" />
            </div>
        </div> `;
    document.body.insertAdjacentHTML("beforeend", templateLightbox);
}

/*==================== HIDDEN LIGHT BOX ====================*/
function HandleHideLightbox(event) {
    if (event.target.matches(".lightbox")) {
        event.target.parentNode.removeChild(event.target);
    }
}


/*==================== ACTIVE SIZE OPTION ====================*/
const sizeItem = document.querySelectorAll(".size-item");
sizeItem && sizeItem.forEach(item => item.addEventListener("click", (e) => {
    sizeItem.forEach(item => {
        item.classList.contains("active") && item.classList.remove("active")
    })

    e.target.classList.add("active");
}))

// Handle quanlity input
function ValidateQuanlity(event) {
    if (parseInt(event.target.value) < 1) {
        alert("Invalid quantity")
        event.target.value = 1;
    }
    if (parseInt(event.target.value) > 10) {
        alert("Can't buy more than 10 product")
        event.target.value = 1;
    }
}


function IncreaseQuanlity(event) {
    const inputVal = document.querySelector('input[name="quantity"]').value;
    if (parseInt(inputVal) >= 10) {
        alert("Can't buy more than 10 product")
        return 0;
    }
    document.querySelector('input[name="quantity"]').value = parseInt(inputVal) + 1;
}


function DecreaseQuanlity(event) {
    const inputVal = document.querySelector('input[name="quantity"]').value;
    if (parseInt(inputVal) <= 1) {
        return 0;
    }
    document.querySelector('input[name="quantity"]').value = parseInt(inputVal) - 1;
}