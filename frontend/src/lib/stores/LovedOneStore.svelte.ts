import type { LovedOne } from '$lib/types/types';
import { getContext, setContext } from 'svelte';

interface LovedOneState {
	lovedOnes: LovedOne[];
	refetchLovedOne: (lovedOne: LovedOne) => void;
}

export class LovedOneStateClass implements LovedOneState {
	lovedOnes = $state<LovedOne[]>([]);

	refetchLovedOne = (lovedOne: LovedOne) => {
		console.log(`kek, gonna try refetching the lovedone for this one ${lovedOne}`);
	};
}

const DEFAULT_KEY = '$_lovedOne_store';

export const getLovedOneState = (key = DEFAULT_KEY) => {
	return getContext<LovedOneState>(key);
};

export const setLovedOneState = (key = DEFAULT_KEY) => {
	const lovedOneState = new LovedOneStateClass();
	return setContext(key, lovedOneState);
};
