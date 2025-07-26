
const wrapper = new Vue({
    el: '#wrapper',
    data: {
        items: [],
        search: "",
        currentPage: 0,
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
                data: { page: wrapper.currentPage, search: wrapper.search },
                url: "/Admin/Ticket/GetData",
                success: function (data) {

                    data.forEach(item => {
                        wrapper.items.push(
                            item
                        );
                    });
                    if (data.length < 1) {
                        wrapper.currentPage--;
                    }
                    wrapper.loadingData = false;
                }
            });
        },
        LoadMore: function () {
            this.currentPage++;
            this.GetData();
        },
        
    }
});
