<script lang="ts">
	import { setHangoutState } from '$lib/stores/HangoutStore.svelte';
	import { setLovedOneState } from '$lib/stores/LovedOneStore.svelte';

	let { children } = $props();

	setHangoutState();
	setLovedOneState();
</script>

{@render children()}
