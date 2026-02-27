<script lang="ts">
	import { getLovedOneState } from '$lib/stores/LovedOneStore.svelte';
	import type { CreateLovedoneRequest, LovedOne } from '$lib/types/types';
	import { createLovedOne } from '../../api/immfapi';

	const lovedOneStore = getLovedOneState();

	async function postLovedOne() {
		const randNum = Math.round(Math.random() * 10000);
		const lovedOne: CreateLovedoneRequest = {
			name: 'Friend' + randNum,
			relationship: 'friend'
		};

		const createdLovedOne = await createLovedOne(lovedOne);
		return createdLovedOne;
	}
	async function addLovedOneToStore(lovedOne: LovedOne) {
		lovedOne != undefined
			? lovedOneStore.addLovedOne(lovedOne)
			: console.log('temp - Loved One Creation did not happen');
	}

	async function addLovedOne() {
		const lovedOne = await postLovedOne();
		lovedOne != undefined
			? await addLovedOneToStore(lovedOne)
			: console.log('temp -lovedone creation didt not happen');
	}
</script>

<button
	onclick={() => {
		addLovedOne();
	}}>Add LovedOne</button
>
