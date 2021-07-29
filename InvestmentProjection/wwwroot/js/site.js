var chart;

var defaultLegendClickHandler = Chart.defaults.plugins.legend.onClick;
var pieDoughnutLegendClickHandler = Chart.controllers.doughnut.overrides.plugins.legend.onClick;
var newLegendClickHandler = function (e, legendItem, legend) {
    var index = legendItem.datasetIndex;
    var type = legend.chart.config.type;

    if (index === 0) {
        // Do the original logic
        if (type === 'pie' || type === 'doughnut') {
            pieDoughnutLegendClickHandler(e, legendItem, legend)
        } else {
            defaultLegendClickHandler(e, legendItem, legend);
        }

    } else if (index === 1) {
        let ci = legend.chart;
        [
            ci.getDatasetMeta(1),
            ci.getDatasetMeta(2)
        ].forEach(function (meta) {
            meta.hidden = meta.hidden === null ? !ci.data.datasets[index].hidden : null;
        });
        ci.update();
    } else if (index === 3) {
        let ci = legend.chart;
        [
            ci.getDatasetMeta(3),
            ci.getDatasetMeta(4)
        ].forEach(function (meta) {
            meta.hidden = meta.hidden === null ? !ci.data.datasets[index].hidden : null;
        });
        ci.update();
    }
};

function createChart(
    labels,
    totalInvestmentData,
    targetValue,
    timeScale,
    growthData)
{
    if (chart) chart.destroy();

    var totalInvestment = {
        label: "Total Investment",
        lineTension: 0.1,
        borderWidth: 3,
        backgroundColor: "rgba(0,0,0,1)",
        borderColor: "rgba(0,0,0,1)",
        fill: false,
        data: totalInvestmentData,
    };

    var growth = {
        label: 'Growth',
        borderColor: 'rgba(0, 24, 176, 0.3)',
        backgroundColor: 'rgba(0, 24, 176, 0.3)',
        fill: false,
        data: growthData,
    };

    var config = {
        type: 'line',
        data: {
            labels: labels,
            datasets: [totalInvestment, growth]
        },
        options: {
            responsive: true,
            scales: {
                x: {
                    type: "linear",
                    display: true,
                    ticks: {
                        stepSize: 1,
                        beginAtZero: true
                    },
                    title: {
                        display: true,
                        text: "Time (Years)"
                    }
                },
                y: {
                    display: true,
                    title: {
                        display: true,
                        text: "Value (£)"
                    }
                }
            },
            elements: {
                point: {
                    radius: 0
                }
            },
            plugins: {
                annotation: {
                    annotations: [
                        {
                            type: 'line',
                            scaleID: 'y',
                            borderWidth: 1,
                            borderColor: 'red',
                            value: targetValue,
                            label: {
                                content: 'Target Value',
                                enabled: true,
                                position: "start",
                            }
                        },
                        {
                            type: 'line',
                            scaleID: 'x',
                            borderWidth: 1,
                            borderColor: 'green',
                            value: timeScale,
                            label: {
                                content: 'End Timescale',
                                enabled: true,
                                position: "end",
                            }
                        }
                    ]
                },
                legend: {
                    onClick: newLegendClickHandler,
                    labels: {
                        filter: function (item, chart) {
                            return item.datasetIndex === 0 || item.datasetIndex === 1 || item.datasetIndex === 3;
                        }
                    }
                },
                tooltip: {
                    enabled: false
                },
            }
        }
    };

    var ctx = document.getElementById("investmentChart").getContext("2d");
    chart = new Chart(ctx, config);
}