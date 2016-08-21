(function (app) {
    'use strict';
    app.directive('componentRating', componentRating);
    function componentRating() {
        return {
            restrict: 'A',
            link: function ($scope, $element, $attrs) {
                $element.raty({
                    score: $attrs.componentRating,
                    halfShow: false,
                    readOnly: $scope.isReadOnly,
                    noRatedMsg: "Not rated yet!",
                    starHalf: "../../Content/raty-2.7.0/images/star-half.png",
                    starOff: "../../Content/raty-2.7.0/images/star-off.png",
                    starOn: "../../Content/raty-2.7.0/images/star-on.png",
                    hints: ["Poor", "Average", "Good", "Very Good", "Excellent"],
                    click: function (score, event) {
                        //Set the model value
                        $scope.movie.Rating = score;
                        $scope.$apply();
                    }
                });
            }
        }
    }
})(angular.module('common.ui'));