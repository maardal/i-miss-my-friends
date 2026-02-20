import { FRIENDS_URL } from '$lib/config';
import { type Hangout } from '$lib/stores/HangoutStore.svelte';

const SUCCESS = 201;
const BAD_REQUEST = 400;

export const createHangout = async (id: string) => {
	console.log(`adding hangout for lovedone: ${id}`);
	const date = formateDateToYYYYMMDD(new Date());

	try {
		const response = await fetch(FRIENDS_URL + 'hangout', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ Date: date, LovedOneId: id })
		});
		if (response.status == BAD_REQUEST) {
			console.log(response.statusText + ': Bad Request - ID or Date no correct.');
			console.warn(`Date: ${date}`);
			console.warn(`ID: ${id}`);
		}
		if (response.status == SUCCESS) {
			const responseData: Hangout = await response.json();
			return responseData;
		}
	} catch (error) {
		console.error(error);
	}
};

const formateDateToYYYYMMDD = (date: Date): string => {
	const year = date.getFullYear();
	const month = String(date.getMonth() + 1).padStart(2, '0');
	const day = String(date.getDate()).padStart(2, '0');

	return `${year}-${month}-${day}`;
};
