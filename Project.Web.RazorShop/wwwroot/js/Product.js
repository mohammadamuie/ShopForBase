const wrapper = new Vue({
    el: '#wrapper',
    data: {
        items: [],
        categories: [],
        search: new URLSearchParams(window.location.search).get('search'),
        currentCategory: new URLSearchParams(window.location.search).get('category'),
        currentPage: 0,
        totalCount: 0,
        pageSize: 0,
        lastPage: 1,
        loadingData: true,
    },
    created: function () {
        this.currentPage++;
    },
    mounted: function () {
        setTimeout(() => {
            wrapper.GetData();
        }, 234);
    },
    methods: {
        GetData: function () {
            $.ajax({
                type: "GET",
                data: { page: wrapper.currentPage, category: wrapper.currentCategory, search: wrapper.search },
                url: "/product/getdata",
                success: function (data) {
                    wrapper.items = [];
                    wrapper.totalCount = data.totalCount;
                    wrapper.pageSize = data.size;
                    data.data.forEach(item => {
                        wrapper.items.push(
                            item
                        );
                    });
                    pagination(wrapper.totalCount, wrapper.currentPage);

                    if (data.length < 1) {
                        wrapper.currentPage--;
                    }
                    wrapper.loadingData = false;
                }
            });
        },
        NumberFormat: function (input) {
            //return input.toLocaleString();
            input = input.toString();
            return input.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
    }
});

function pagination(totalPages, currentPage) {

    if (!totalPages)
        totalPages = 1

    if (!currentPage)
        currentPage = 1

    $("#luckmoshy").twbsPagination('destroy');
    $('#luckmoshy').twbsPagination({
        totalPages: totalPages,
        startPage: currentPage,
        visiblePages: 3,
        onPageClick: function (event, page) {
            wrapper.currentPage = page;
            wrapper.GetData();
        },
        initiateStartPageClick: false,
        href: false,
        first: 'اولین',
        prev: 'قبلی',
        next: 'بعدی',
        last: 'آخرین',

        paginationClass: '',
        pageClass: 'page-item',        
        activeClass: 'active text-white',
        loop: false,
        disabledClass: 'disabled',
        anchorClass: 'page-link',

    });

}

