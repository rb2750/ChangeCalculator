export default class Home {
	constructor() {
		this.handleFormSubmit();
		this.handleInputChangeFinishEditing();
	}

	/**
	 * Handle the 'calculate' button press, so that we can show a custom, more pretty response.
	 */
	private handleFormSubmit(): void {
		$(document).on('submit', '#calculation-form', function () {
			const form = $(this);

			$.ajax({
				url: form.attr('action'), // The action (generated by asp)
				type: 'POST',
				data: form.serialize(), // Data from the form
				success(response) {
					if (response.success) {
						Home.displayCard('Your change is:', Home.generateHTMLChangeListFromObject(response.result), false);
					} else {
						Home.displayCard('There was an error', response.message, true);
					}
				}
			});
			return false;
		});
	}

	/**
	 * Generate a HTML list given the response from the serve
	 * @param input The "change" array returned from the server
	 */
	private static generateHTMLChangeListFromObject(input: Record<string, unknown>) {
		if (Object.keys(input).length === 0) {
			return 'No change given.';
		}

		const listInstance = $('<ul></ul>');

		Object.keys(input).forEach((key) => {
			listInstance.append($(`<li>${input[key]}x ${key}</li>`));
		});

		return listInstance.html();
	}

	/**
	 * When the inputs are clicked off, this corrects the values to 2DP.
	 */
	private handleInputChangeFinishEditing(): void {
		$(document).on('focusout', '#calculation-form input', function () {
			$(this).val(parseFloat(($(this).val() as string) || '0' /* If null then 0 to prevent NaN error */).toFixed(2));
		});
	}

	/**
	 * Slides down a card
	 * @param header The text to show as the card header
	 * @param content The content to show in the card
	 * @param error whether the card should be red
	 */
	private static displayCard(header: string, content: string, error: boolean | false) {
		const card = $('#response-card');

		// Set the header and body.
		card.find('.card-header').html(header);
		card.find('.card-body').html(content);

		// If it's of an error type, set the relevant classes, else remove them.
		if (error) {
			card.addClass('bg-danger text-white');
		} else {
			card.removeClass('bg-danger text-white');
		}

		// Animate the card sliding down.
		card.slideDown();
	}
}
