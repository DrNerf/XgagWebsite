$(window).scroll(function () {
    if ($(window).scrollTop() + $(window).height() === $(document).height()) {
        console.log("Reached end of posts page, loading more.");
        PostsLoader.loadNextPage();
    }
});

PostsLoader = {
    currentPage: 1,
    loadNextPage: function () {
        var self = PostsLoader;
        self.currentPage++;

        $.get("/Home/Posts?page=" + self.currentPage, function (response) {
            $("#posts-container").append(response);
            self.listenForLoadingImages();
        });
    },
    listenForLoadingImages: function () {
        $('.post-image').filter(function () { return !$(this).data('loaded'); }).one('load', function () {
            var postId = $(this).attr('data-id');
            $(this).data('loaded', true);
            if (postId) {
                var overlay = $('#loading-overlay-' + postId);
                if (overlay) {
                    overlay.fadeOut(1000);
                }
            }
        }).each(function () {
            if (this.complete) $(this).load();
        });
    }
};

PostsLoader.listenForLoadingImages();