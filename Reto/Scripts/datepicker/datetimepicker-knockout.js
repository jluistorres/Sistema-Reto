ko.bindingHandlers.dateTimePicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().dateTimePickerOptions || {};
        $(element).datetimepicker(options);

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "dp.change", function (event) {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                if (event.date && event.date != null && !(event.date instanceof Date)) {
                    value(event.date.toDate());
                } else {
                    value(undefined);
                }
            }
        });        

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            var picker = $(element).data("DateTimePicker");
            if (picker) {
                picker.destroy();
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var picker = $(element).data("DateTimePicker");
        //when the view model is updated, update the widget
        if (picker) {
            //Usamos moment para convertir a fecha ya que utiliza este plugin adicional datetimepicker
            var koDate = toDate(ko.utils.unwrapObservable(valueAccessor()));            
            picker.date(koDate);
        }
    }
};

ko.bindingHandlers.rangePicker = {
    update: function (element, valueAccessor, allBindings) {
        var picker = $(element).data("DateTimePicker");
        var range = ko.utils.unwrapObservable(valueAccessor());        
        var min, max;
        
        if (range.min && ko.utils.unwrapObservable(range.min)) {            
            min = toDate(ko.utils.unwrapObservable(range.min));
            //A partir de las 00:00:00 hrs
            min.setHours(0, 0, 0, 0);

            var maxDate = toDate(picker.maxDate()); //ko.utils.unwrapObservable(range.max)
            if (maxDate && moment(min).isAfter(maxDate)) {
                maxDate = moment(min).format('DD/MM/YYYY');
                //if (range.max) { range.max(maxDate); }
                picker.maxDate(maxDate);
            }
        }
        if (range.max && ko.utils.unwrapObservable(range.max)) {
            max = toDate(ko.utils.unwrapObservable(range.max));
            //Hasta las 23:59:59 hrs
            max.setHours(23, 59, 59, 999);

            var minDate = toDate(picker.minDate()); // ko.utils.unwrapObservable(range.min)
            if (minDate && moment(max).isBefore(minDate)) {
                minDate = moment(max).format('DD/MM/YYYY');
                //if (range.min) { range.min(minDate); }
                picker.minDate(minDate);
            }
        }

        if(min) picker.minDate(min);
        if(max) picker.maxDate(max);        
    }
};

ko.bindingHandlers.highLightPicker = {
    update: function (element, valueAccessor) {        
        //var picker = $element.data("DateTimePicker");
        var range = ko.utils.unwrapObservable(valueAccessor());
        var min = ko.utils.unwrapObservable(range.min),
            max = ko.utils.unwrapObservable(range.max);

        //console.log(min, max);
        if (min && max) {
            min = toDate(min).setHours(0, 0, 0, 0);
            max = toDate(max).setHours(23, 59, 59, 999);
            $(element).find('[data-day]').each(function (el, index) {
                $(this).removeClass('highlight');
                var day = moment($(this).data('day'), 'DD/MM/YYYY').toDate();
                if (day >= min && day <= max) {
                    $(this).addClass('highlight');
                }
            });
        }
    }
};

var toDate = function (value) {
    if (typeof value === 'string') {
        var paternDefault = /\/Date\(-?(\d+)(-\d+)?\)\//;
        var paternString = /(\d{2})\/(\d{2})\/(\d{4})/;
        var patternISO = /^(\d{4})(?:-?W(\d+)(?:-?(\d+)D?)?|(?:-(\d+))?-(\d+))(?:[T ](\d+):(\d+)(?::(\d+)(?:\.(\d+))?)?)?(?:Z(-?\d*))?$/; //--> /(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})/;
        if (paternDefault.test(value)) {            
            return moment(value).toDate();
        }
        else if (paternString.test(value)) {            
            return moment(value, 'DD/MM/YYYY hh:mm:ss a').toDate();
        }
        else if (patternISO.test(value)) {           
            return moment(value, moment.ISO_8601).toDate();
        }
    } else if (value && value.toDate) {//Moment.toDate
        return value.toDate();
    }
    return value || null;
}