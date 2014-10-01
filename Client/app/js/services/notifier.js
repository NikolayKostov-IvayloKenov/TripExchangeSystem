tripExchange.factory('notifier', ['toastr', function(toastr) {
    toastr.options = {
        "positionClass": "toast-top-full-width"
    }

    return {
        success: function(msg) {
            toastr.success(msg);
        },
        error: function(msg) {
            toastr.error(msg);
        }
    }
}])