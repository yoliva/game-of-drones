angular.module('gameOfDrones').
    controller('ConfigurationController', [
        '$scope', 'ruleDataService', function($scope, ruleDataService) {

            $scope.currentRule = -1;
            $scope.availableRules = [];
            $scope.newRule = {};

            ruleDataService.getAll()
                .success(function(data) {

                    $.each(data, function(index, item) {
                        $scope.availableRules.push({
                            value: item.id,
                            text: item.name
                        });
                    });

                });

            $scope.setCurrentRule = function() {
                ruleDataService.updateCurrent($scope.currentRule).success(function(data) {
                    swal("Success!", data.name + ' is the current rule now.', "success");
                });
            };

            $scope.createRule = function() {
                var rule = {
                    name: $scope.newRule.name,
                    ruleDefinition: $scope.newRule.ruleDefinition
                };
                ruleDataService.create(rule)
                    .success(function(data) {
                        swal("Success!", data.name + ' was successfully created.', "success");
                        $scope.availableRules.push({
                            value: data.id,
                            text: data.name
                        });

                        //reset form inputs
                        $scope.newRule.ruleDefinition = '';
                        $scope.newRule.name = '';
                    });
            };
        }
    ]);