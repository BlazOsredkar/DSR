// Naloga: jQuery UI komponente (navodilo: "accordion", "slider", "tabs", "koledar").
$(function () {
	$(".js-accordion").accordion({
		heightStyle: "content",
		collapsible: true,
		active: false
	});

	$(".js-tabs").tabs();

	$(".js-datepicker").datepicker({
		dateFormat: "dd.mm.yy"
	});

	$(".js-slider").each(function () {
		var $slider = $(this);
		var targetSelector = $slider.data("target");
		if (!targetSelector) {
			return;
		}

		var $input = $(targetSelector);
		var min = parseFloat($input.data("min")) || 0;
		var max = parseFloat($input.data("max")) || 100;
		var step = parseFloat($input.data("step")) || 1;
		var current = parseFloat(($input.val() || "").toString().replace(",", "."));
		if (isNaN(current)) {
			current = min;
		}

		$slider.slider({
			min: min,
			max: max,
			step: step,
			value: current,
			slide: function (_, ui) {
				$input.val(formatSliderValue(ui.value, step));
			}
		});

		$input.on("change", function () {
			var value = parseFloat(($input.val() || "").toString().replace(",", "."));
			if (!isNaN(value)) {
				$slider.slider("value", value);
			}
		});
	});
});

function formatSliderValue(value, step) {
	var decimals = 0;
	var stepText = step.toString();
	if (stepText.indexOf(".") >= 0) {
		decimals = stepText.split(".")[1].length;
	}

	return decimals > 0 ? value.toFixed(decimals) : value.toString();
}
