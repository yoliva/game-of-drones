angular.module('gameOfDrones').
    controller('ConfigurationController', [
        '$scope', 'ruleDataService', function($scope, ruleDataService) {

            $scope.currentRule = {};
            $scope.rules = [];
            $scope.availableRules = [];
            $scope.newRule = {};

            ruleDataService.getAll()
                .success(function(data) {
                    $scope.rules = data;
                    $.each(data, function(index, item) {
                        $scope.availableRules.push({
                            value: item.id,
                            text: item.name
                        });
                    });

                });

            $scope.setCurrentRule = function() {
                ruleDataService.updateCurrent($scope.currentRule.id).success(function (data) {
                    if (data.isValid)
                        swal("Success!", data.rule.name + ' is the current rule now.', "success");
                    else 
                        swal("Error!", data.rule.name + ' has an invalid rule definition.', "error");
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

            $scope.displayRuleDefinition = function () {
                $scope.currentRule.definition = $scope.rules.filter(function(item) { return item.id == $scope.currentRule.id; })[0].ruleDefinition;
            }
        }
    ]);