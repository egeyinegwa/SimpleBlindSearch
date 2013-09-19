﻿$(function () {

    //based on RP Niemeyer answer 
    //http://stackoverflow.com/questions/7537002/autocomplete-combobox-with-knockout-js-template-jquery

    //jqAuto -- main binding (should contain additional options to pass to autocomplete)
    //jqAutoSource -- the array to populate with choices (needs to be an observableArray)
    //jqAutoQuery -- function to return choices
    //jqAutoValue -- where to write the selected value
    //jqAutoSourceLabel -- the property that should be displayed in the possible choices
    //jqAutoSourceInputValue -- the property that should be displayed in the input box
    //jqAutoSourceValue -- the property to use for the value

    ko.bindingHandlers.jqAuto = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var options = valueAccessor() || {},
                allBindings = allBindingsAccessor(),
                unwrap = ko.utils.unwrapObservable,
                modelValue = allBindings.jqAutoValue,
                source = allBindings.jqAutoSource,
                query = allBindings.jqAutoQuery,
                valueProp = allBindings.jqAutoSourceValue,
                inputValueProp = allBindings.jqAutoSourceInputValue || valueProp,
                labelProp = allBindings.jqAutoSourceLabel || inputValueProp;

            //function that is shared by both select and change event handlers
            function writeValueToModel(valueToWrite) {
                if (ko.isWriteableObservable(modelValue)) {
                    modelValue(valueToWrite);
                } else {  //write to non-observable
                    if (allBindings['_ko_property_writers'] && allBindings['_ko_property_writers']['jqAutoValue'])
                        allBindings['_ko_property_writers']['jqAutoValue'](valueToWrite);
                }
            }

            //on a selection write the proper value to the model
            options.select = function (event, ui) {
                writeValueToModel(ui.item ? ui.item.actualValue : null);
            };

            //on a change, make sure that it is a valid value or clear out the model value
            options.change = function (event, ui) {
                var currentValue = $(element).val();
                var matchingItem = ko.utils.arrayFirst(unwrap(source), function (item) {
                    return unwrap(inputValueProp ? item[inputValueProp] : item) === currentValue;
                });

                if (!matchingItem) {
                    writeValueToModel(null);
                }
            };

            //hold the autocomplete current response
            var currentResponse = null;

            //handle the choices being updated in a DO, to decouple value updates from source (options) updates
            var mappedSource = ko.dependentObservable({
                read: function () {
                    mapped = ko.utils.arrayMap(unwrap(source), function (item) {
                        var result = {};
                        result.label = labelProp ? unwrap(item[labelProp]) : unwrap(item).toString();  //show in pop-up choices
                        result.value = inputValueProp ? unwrap(item[inputValueProp]) : unwrap(item).toString();  //show in input box
                        result.actualValue = valueProp ? unwrap(item[valueProp]) : item;  //store in model
                        return result;
                    });
                    return mapped;
                },
                write: function (newValue) {
                    source(newValue);  //update the source observableArray, so our mapped value (above) is correct
                    if (currentResponse) {
                        currentResponse(mappedSource());
                    }
                },
                disposeWhenNodeIsRemoved: element
            });

            if (query) {
                options.source = function (request, response) {
                    currentResponse = response;
                    query.call(this, request.term, mappedSource);
                };
            } else {
                //whenever the items that make up the source are updated, make sure that autocomplete knows it
                mappedSource.subscribe(function (newValue) {
                    $(element).autocomplete("option", "source", newValue);
                });

                options.source = mappedSource();
            }


            //initialize autocomplete
            $(element).autocomplete(options);
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            //update value based on a model change
            var allBindings = allBindingsAccessor(),
                unwrap = ko.utils.unwrapObservable,
                modelValue = unwrap(allBindings.jqAutoValue) || '',
                valueProp = allBindings.jqAutoSourceValue,
                inputValueProp = allBindings.jqAutoSourceInputValue || valueProp;

            //if we are writing a different property to the input than we are writing to the model, then locate the object
            if (valueProp && inputValueProp !== valueProp) {
                var source = unwrap(allBindings.jqAutoSource) || [];
                var modelValue = ko.utils.arrayFirst(source, function (item) {
                    return unwrap(item[valueProp]) === modelValue;
                }) || {};
            }

            //update the element with the value that should be shown in the input
            $(element).val(modelValue && inputValueProp !== valueProp ? unwrap(modelValue[inputValueProp]) : modelValue.toString());
        }
    };

    //JIcons: http://jquery-ui.googlecode.com/svn/tags/1.6rc5/tests/static/icons.html

    ko.bindingHandlers.jQButton = {
        init: function (element, valueAccessor) {
            var options = valueAccessor() || {};
            $(element).button(options);

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).button("destroy");
            });
        }
    };

    ko.bindingHandlers.jqTabs = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var dependency = ko.utils.unwrapObservable(valueAccessor());
            var currentIndex = ($(element).data("ui-tabs")) ? ($(element).tabs("option", "active")) : 0;
            var config = ko.utils.unwrapObservable(allBindingsAccessor().jqTabOptions) || {};

            //make sure that elements are set from template before calling tabs API
            setTimeout(function () {
                if ($(element).data("ui-tabs")) {
                    $(element).tabs("destroy").tabs(config).tabs("option", "active", currentIndex);
                }
                else {
                    $(element).tabs(config).tabs("option", "active", currentIndex);
                }

            }, 0);
        }
    };

    ko.bindingHandlers.chosen = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var allBindings = allBindingsAccessor();
            var options = { default1: 'Please select...' };
            $.extend(options, allBindings.chosen);
            $(element).attr('data-placeholder', options.default1);
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            $(element).chosen();
            $(element).trigger("liszt:updated");
        }
    };
});