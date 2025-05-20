import type { PageLoad } from './$types';
import { FRIENDS_URL } from '$lib/config';

export const load: PageLoad = async ({ fetch }) => {
	const response = await fetch(FRIENDS_URL);
	const body = await response.json();
	return { lovedones: body };
};
