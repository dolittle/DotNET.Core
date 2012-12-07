﻿Bifrost.features.featureManager.get("Content/index").defineViewModel(function () {
    var self = this;
    this.currentFeature = ko.observable("home");
    this.currentFeaturePath = ko.computed(function () {
        var path = self.currentFeature() + "/index";
        return path;
    });

    Bifrost.messaging.Messenger.global.subscribeTo("currentFeatureChanged", function (feature) {
        self.currentFeature(feature);
    });
});