'use strict';

tripExchange.directive('datePicker', function() {
    return {
        restrict: 'A',
        link: function(scope, element) {
            element.datetimepicker({
                dateFormat: 'yy-mm-dd',
                timeFormat: 'HH:mm:ss',
                minDate: new Date()
            });
        }
    }
});