'use strict';

var sessionAlert = $("#dialog-session");

/* module def */
var sessionApp = angular.module('sessionApp', []);
var sessionAppTimeoutId;

/* contants */
//sessionApp.constant('settings', { threshold: 3000, sessionDuration: 5000 });

/* filters */
sessionApp.filter('minute', function () {
    return function (milliseconds) {
        var seconds = Math.floor(milliseconds / 1000);
        var minutes = Math.floor(seconds / 60);
        seconds = seconds % 60;
        return minutes + ":" + (seconds < 10 ? "0" + seconds : seconds);
    }
});

/* controllers */
sessionApp.controller('SessionCtrl', ['$scope', '$timeout', '$window', 'settings', function ($scope, $timeout, $window, settings) {

    $scope.checkInterval = 1000;
    $scope.threshold = settings.threshold;
    $scope.duration = $scope.remainingTime = settings.sessionDuration;

    $scope.hide = function () {
        sessionAlert.dialog("close");
    }
    $scope.displayDialog = function () {

        //remove logging in production
        console.log('displayDialog');

        sessionAlert.dialog({
            dialogClass: "dnnFormPopup",
            modal: true,
            resizable: false,
            buttons: [{
                text: "Extend Session",
                click: function () {
                    $window.location.reload();
                }
            }]
        });
    }
    $scope.checkSession = function () {
        $timeout.cancel(sessionAppTimeoutId);

        $scope.remainingTime = $scope.remainingTime - $scope.checkInterval;

        //remove logging in production
        console.log($scope.remainingTime);

        if ($scope.remainingTime === settings.threshold) {
            $scope.displayDialog();
        }

        if ($scope.remainingTime > 0) {
            sessionAppTimeoutId = $timeout($scope.checkSession, $scope.checkInterval);
        } else {
            sessionAlert.dialog("option", "buttons", [{ text: "Ok", click: function () { $window.location.reload(); } }]);
        }
    }
    $scope.start = function () {
        sessionAppTimeoutId = $timeout($scope.checkSession, $scope.checkInterval);
    }

    $scope.start();

}]);