import type { PageLoad } from './$types';
import type { LovedOne } from '$lib/types/types';
import { FRIENDS_URL } from '$lib/config';

type ApiResponse = { lovedOnes: LovedOne[] };

export const load: PageLoad = async ({ fetch }) => {
	try {
		const response = await fetch(FRIENDS_URL + 'lovedones/');
		const body: ApiResponse = await response.json();

		return body;
	} catch (error) {
		console.warn(error);
		return { lovedOnes: [] as LovedOne[] };
	}
};
