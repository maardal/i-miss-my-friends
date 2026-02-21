import type { Hangout } from '$lib/types/types';
import { getContext, setContext } from 'svelte';

interface HangoutState {
	hangouts: Hangout[];
	addHangout: (hangout: Hangout) => void;
	getHangoutsByLovedOneId: (lovedOneId: string) => Hangout[];
}

export class HangoutStateClass implements HangoutState {
	hangouts = $state<Hangout[]>([]);

	addHangout = (hangout: Hangout) => {
		const savedHangout = this.hangouts.find((savedHangout) => savedHangout.id == hangout.id);
		savedHangout
			? console.log(`Hangout with ID ${hangout.id} already exists.`)
			: this.hangouts.push(hangout);
	};

	getHangoutsByLovedOneId = (lovedOneId: string) => {
		return this.hangouts.filter((hangout) => hangout.lovedOneId == lovedOneId);
	};
}

const DEFAULT_KEY = '$_hangout_state';

export const getHangoutState = (key = DEFAULT_KEY) => {
	return getContext<HangoutState>(key);
};

export const setHangoutState = (key = DEFAULT_KEY) => {
	const hangoutState = new HangoutStateClass();
	return setContext(key, hangoutState);
};
