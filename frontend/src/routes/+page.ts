import type { PageLoad } from './$types';
import { BASE_BACKEND_URL } from '$lib/config';

export const load: PageLoad = async ({ fetch }) => {
	let lovedones = [];
	try {
		const response = await fetch(BASE_BACKEND_URL + "lovedones/");
		lovedones = await response.json();
	} catch (error) {
		console.log("Error loading data from backend =>", error);
	}
	return { lovedones: [...lovedones] };
};
